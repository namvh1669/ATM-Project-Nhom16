using System;
using System.IO;
using System.Security.Cryptography;

namespace ATM.Common
{
    /// <summary>
    /// [Rubric: Cryptography + I/O Network + Socket Logic]
    /// Kênh truyền bảo mật trên TCP (NetworkStream):
    ///   1. Server gửi khóa công khai RSA-2048.
    ///   2. Client sinh ngẫu nhiên 64 byte (khóa AES-256 + khóa HMAC-SHA256),
    ///      mã hóa bằng RSA-OAEP(SHA-256) rồi gửi lên Server.
    ///   3. Mọi gói tin sau đó: AES-256-CBC + HMAC-SHA256 (encrypt-then-MAC),
    ///      kèm số thứ tự và hướng gửi trong MAC để chống sửa gói / phát lại gói.
    /// Khung dữ liệu (frame): [4 byte độ dài big-endian][payload].
    /// Load Balancer chỉ chuyển tiếp các byte này nên không đọc được nội dung.
    /// </summary>
    public sealed class SecureChannel
    {
        private const int MaxFrame = 1 << 20;
        private const int IvSize = 16;
        private const int MacSize = 32;
        private const string Magic = "ATM-SEC-1";

        private readonly Stream _stream;
        private readonly byte[] _encKey;
        private readonly byte[] _macKey;
        private readonly byte _sendDir;
        private readonly byte _recvDir;
        private long _sendSeq;
        private long _recvSeq;
        private readonly object _sendLock = new object();

        public string PeerFingerprint { get; private set; }

        private SecureChannel(Stream stream, byte[] encKey, byte[] macKey, bool isClient)
        {
            _stream = stream;
            _encKey = encKey;
            _macKey = macKey;
            _sendDir = (byte)(isClient ? 1 : 2);
            _recvDir = (byte)(isClient ? 2 : 1);
        }

        // ---------------------------------------------------------------- frame

        public static void WriteFrame(Stream s, byte[] payload)
        {
            byte[] buf = new byte[4 + payload.Length];
            int len = payload.Length;
            buf[0] = (byte)(len >> 24);
            buf[1] = (byte)(len >> 16);
            buf[2] = (byte)(len >> 8);
            buf[3] = (byte)len;
            Buffer.BlockCopy(payload, 0, buf, 4, payload.Length);
            s.Write(buf, 0, buf.Length);
            s.Flush();
        }

        public static byte[] ReadFrame(Stream s)
        {
            byte[] hdr = new byte[4];
            ReadExact(s, hdr, 4);
            int len = (hdr[0] << 24) | (hdr[1] << 16) | (hdr[2] << 8) | hdr[3];
            if (len < 0 || len > MaxFrame) throw new InvalidDataException("Độ dài gói tin không hợp lệ: " + len);
            byte[] data = new byte[len];
            ReadExact(s, data, len);
            return data;
        }

        private static void ReadExact(Stream s, byte[] buf, int count)
        {
            int off = 0;
            while (off < count)
            {
                int n = s.Read(buf, off, count - off);
                if (n <= 0) throw new EndOfStreamException("Kết nối đã bị đóng.");
                off += n;
            }
        }

        // ------------------------------------------------------------ handshake

        /// <summary>Phía Server: gửi khóa công khai, nhận khóa phiên do Client mã hóa bằng RSA.</summary>
        public static SecureChannel AcceptAsServer(Stream s, RSA serverRsa)
        {
            RSAParameters pub = serverRsa.ExportParameters(false);
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(Magic);
                w.Write(pub.Modulus.Length);
                w.Write(pub.Modulus);
                w.Write(pub.Exponent.Length);
                w.Write(pub.Exponent);
                w.Flush();
                WriteFrame(s, ms.ToArray());
            }

            byte[] encrypted = ReadFrame(s);
            byte[] keys;
            lock (serverRsa)
            {
                keys = serverRsa.Decrypt(encrypted, RSAEncryptionPadding.OaepSHA256);
            }
            if (keys.Length != 64) throw new CryptographicException("Khóa phiên không hợp lệ.");

            byte[] enc = new byte[32];
            byte[] mac = new byte[32];
            Buffer.BlockCopy(keys, 0, enc, 0, 32);
            Buffer.BlockCopy(keys, 32, mac, 0, 32);
            var ch = new SecureChannel(s, enc, mac, false);
            ch.PeerFingerprint = CryptoUtil.Fingerprint(pub.Modulus);
            return ch;
        }

        /// <summary>Phía Client: nhận khóa công khai của Server, gửi khóa phiên đã mã hóa RSA.</summary>
        public static SecureChannel ConnectAsClient(Stream s)
        {
            byte[] hello = ReadFrame(s);
            byte[] modulus;
            byte[] exponent;
            using (var ms = new MemoryStream(hello))
            using (var r = new BinaryReader(ms))
            {
                if (r.ReadString() != Magic) throw new InvalidDataException("Máy chủ không phải ATM Server (sai giao thức).");
                int ml = r.ReadInt32();
                if (ml < 64 || ml > 1024) throw new InvalidDataException("Khóa công khai không hợp lệ.");
                modulus = r.ReadBytes(ml);
                int el = r.ReadInt32();
                if (el < 1 || el > 16) throw new InvalidDataException("Khóa công khai không hợp lệ.");
                exponent = r.ReadBytes(el);
            }

            byte[] keys = CryptoUtil.RandomBytes(64);
            byte[] encrypted;
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(new RSAParameters { Modulus = modulus, Exponent = exponent });
                encrypted = rsa.Encrypt(keys, RSAEncryptionPadding.OaepSHA256);
            }
            WriteFrame(s, encrypted);

            byte[] enc = new byte[32];
            byte[] mac = new byte[32];
            Buffer.BlockCopy(keys, 0, enc, 0, 32);
            Buffer.BlockCopy(keys, 32, mac, 0, 32);
            var ch = new SecureChannel(s, enc, mac, true);
            ch.PeerFingerprint = CryptoUtil.Fingerprint(modulus);
            return ch;
        }

        // ------------------------------------------------------- send / receive

        public void Send<T>(T message)
        {
            byte[] plain = Json.ToBytes(message);
            lock (_sendLock)
            {
                byte[] iv = CryptoUtil.RandomBytes(IvSize);
                byte[] cipher;
                using (var aes = Aes.Create())
                {
                    aes.Key = _encKey;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    using (var enc = aes.CreateEncryptor())
                    {
                        cipher = enc.TransformFinalBlock(plain, 0, plain.Length);
                    }
                }
                byte[] mac = ComputeMac(_sendDir, _sendSeq, iv, cipher);
                _sendSeq++;

                byte[] frame = new byte[IvSize + cipher.Length + MacSize];
                Buffer.BlockCopy(iv, 0, frame, 0, IvSize);
                Buffer.BlockCopy(cipher, 0, frame, IvSize, cipher.Length);
                Buffer.BlockCopy(mac, 0, frame, IvSize + cipher.Length, MacSize);
                WriteFrame(_stream, frame);
            }
        }

        public T Receive<T>()
        {
            byte[] frame = ReadFrame(_stream);
            int cipherLen = frame.Length - IvSize - MacSize;
            if (cipherLen < 16 || cipherLen % 16 != 0) throw new InvalidDataException("Gói tin mã hóa không hợp lệ.");

            byte[] iv = new byte[IvSize];
            byte[] cipher = new byte[cipherLen];
            byte[] mac = new byte[MacSize];
            Buffer.BlockCopy(frame, 0, iv, 0, IvSize);
            Buffer.BlockCopy(frame, IvSize, cipher, 0, cipherLen);
            Buffer.BlockCopy(frame, IvSize + cipherLen, mac, 0, MacSize);

            // Kiểm tra MAC TRƯỚC khi giải mã (encrypt-then-MAC)
            byte[] expected = ComputeMac(_recvDir, _recvSeq, iv, cipher);
            if (!CryptoUtil.FixedTimeEquals(mac, expected))
                throw new CryptographicException("Gói tin bị sửa đổi hoặc bị phát lại (MAC không khớp).");
            _recvSeq++;

            byte[] plain;
            using (var aes = Aes.Create())
            {
                aes.Key = _encKey;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (var dec = aes.CreateDecryptor())
                {
                    plain = dec.TransformFinalBlock(cipher, 0, cipher.Length);
                }
            }
            return Json.FromBytes<T>(plain);
        }

        private byte[] ComputeMac(byte dir, long seq, byte[] iv, byte[] cipher)
        {
            byte[] data = new byte[1 + 8 + iv.Length + cipher.Length];
            data[0] = dir;
            for (int i = 0; i < 8; i++) data[1 + i] = (byte)(seq >> (56 - 8 * i));
            Buffer.BlockCopy(iv, 0, data, 9, iv.Length);
            Buffer.BlockCopy(cipher, 0, data, 9 + iv.Length, cipher.Length);
            using (var h = new HMACSHA256(_macKey))
            {
                return h.ComputeHash(data);
            }
        }
    }
}
