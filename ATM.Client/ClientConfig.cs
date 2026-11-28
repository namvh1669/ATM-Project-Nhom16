using System;
using System.IO;
using System.Text.Json;

namespace ATM.Client.Net
{
    /// <summary>
    /// [Rubric: I/O (File) + Demo via LAN/Internet]
    /// Cấu hình địa chỉ Load Balancer lưu ở %LocalAppData%\ATMDemo\client.config.json.
    /// LAN: nhập IP LAN của máy chạy Load Balancer. Internet: nhập IP/tên miền công cộng (hoặc địa chỉ tunnel).
    /// </summary>
    public class ClientConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string LastCard { get; set; }

        public ClientConfig()
        {
            Host = "127.0.0.1";
            Port = 9000;
            LastCard = "";
        }

        private static string FilePath
        {
            get
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ATMDemo");
                return Path.Combine(dir, "client.config.json");
            }
        }

        public static ClientConfig Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    ClientConfig c = JsonSerializer.Deserialize<ClientConfig>(File.ReadAllText(FilePath));
                    if (c != null && !string.IsNullOrWhiteSpace(c.Host) && c.Port > 0) return c;
                }
            }
            catch { }
            return new ClientConfig();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                File.WriteAllText(FilePath, JsonSerializer.Serialize(this));
            }
            catch { }
        }
    }
}
