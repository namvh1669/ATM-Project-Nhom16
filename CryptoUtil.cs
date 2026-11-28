using System;
using System.Security.Cryptography;
using System.Text;

namespace ATM.Common
{
    /// <summary>
    /// [Rubric: Cryptography] Các hàm mật mã dùng chung:
    /// băm mã PIN bằng PBKDF2-HMAC-SHA256 (có salt), so sánh thời gian hằng số, sinh số ngẫu nhiên an toàn.
    /// </summary>
    public static class CryptoUtil
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static byte[] RandomBytes(int count)
        {
            byte[] b = new byte[count];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(b);
            }
            return b;
        }

        /// <summary>Sinh chuỗi gồm n chữ số ngẫu nhiên (dùng cho số thẻ).</summary>
        public static string RandomDigits(int n)
        {
            byte[] b = RandomBytes(n);
            var sb = new StringBuilder(n);
            for (int i = 0; i < n; i++) sb.Append((char)('0' + (b[i] % 10)));
            return sb.ToString();
        }

        private static byte[] Pbkdf2(string pin, byte[] salt, int iterations)
        {
            using (var kdf = new Rfc2898DeriveBytes(pin, salt, iterations, HashAlgorithmName.SHA256))
            {
                return kdf.GetBytes(HashSize);
            }
        }

        /// <summary>Trả về chuỗi "iterations.salt.hash" (Base64) để lưu vào Database.</summary>
        public static string HashPin(string pin)
        {
            byte[] salt = RandomBytes(SaltSize);
            byte[] hash = Pbkdf2(pin, salt, Iterations);
            return Iterations + "." + Convert.ToBase64String(salt) + "." + Convert.ToBase64String(hash);
        }

        public static bool VerifyPin(string pin, string stored)
        {
            try
            {
                string[] parts = (stored ?? "").Split('.');
                if (parts.Length != 3) return false;
                int iter = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] expected = Convert.FromBase64String(parts[2]);
                byte[] actual = Pbkdf2(pin ?? "", salt, iter);
                return FixedTimeEquals(expected, actual);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>So sánh hai mảng byte trong thời gian không phụ thuộc vào nội dung (chống timing attack).</summary>
        public static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }

        /// <summary>Vân tay khóa công khai: 8 byte đầu của SHA-256, dạng "AB:CD:...".</summary>
        public static string Fingerprint(byte[] data)
        {
            using (var sha = SHA256.Create())
            {
                byte[] h = sha.ComputeHash(data);
                var sb = new StringBuilder();
                for (int i = 0; i < 8; i++)
                {
                    if (i > 0) sb.Append(':');
                    sb.Append(h[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
