using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ATM.LoadBalancer
{
    public class BackendConfig
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    /// <summary>[Rubric: I/O (File)] Cấu hình Load Balancer lưu tại %LocalAppData%\ATMDemo\lb.config.json.</summary>
    public class LbConfig
    {
        public int ListenPort { get; set; }
        public string Algorithm { get; set; }
        public List<BackendConfig> Backends { get; set; }

        public LbConfig()
        {
            ListenPort = 9000;
            Algorithm = "RoundRobin";
            Backends = new List<BackendConfig>();
            Backends.Add(new BackendConfig { Name = "Server-1", Host = "127.0.0.1", Port = 9101 });
            Backends.Add(new BackendConfig { Name = "Server-2", Host = "127.0.0.1", Port = 9102 });
        }

        private static string FilePath
        {
            get
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ATMDemo");
                return Path.Combine(dir, "lb.config.json");
            }
        }

        public static LbConfig Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    LbConfig c = JsonSerializer.Deserialize<LbConfig>(File.ReadAllText(FilePath));
                    if (c != null && c.Backends != null && c.ListenPort > 0) return c;
                }
            }
            catch { }
            return new LbConfig();
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
