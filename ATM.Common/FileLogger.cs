using System;
using System.IO;
using System.Text;

namespace ATM.Common
{
    /// <summary>
    /// [Rubric: I/O (File)] Ghi nhật ký ra file logs\{prefix}-yyyyMMdd.log (an toàn đa luồng)
    /// và phát sự kiện để giao diện hiển thị theo thời gian thực.
    /// </summary>
    public sealed class FileLogger
    {
        private readonly string _dir;
        private readonly string _prefix;
        private readonly object _lock = new object();

        /// <summary>Được gọi từ nhiều luồng khác nhau - giao diện phải tự Invoke về luồng UI.</summary>
        public event Action<string> LineWritten;

        public string Directory { get { return _dir; } }

        public FileLogger(string prefix)
        {
            _prefix = prefix;
            _dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            try { System.IO.Directory.CreateDirectory(_dir); } catch { }
        }

        public void Info(string message) { Write("INFO", message); }
        public void Warn(string message) { Write("WARN", message); }
        public void Error(string message) { Write("ERROR", message); }

        private void Write(string level, string message)
        {
            string line = DateTime.Now.ToString("HH:mm:ss") + " [" + level + "] " + message;
            lock (_lock)
            {
                try
                {
                    string path = Path.Combine(_dir, _prefix + "-" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                    File.AppendAllText(path, line + Environment.NewLine, Encoding.UTF8);
                }
                catch
                {
                    // Không để lỗi ghi log làm sập chương trình
                }
            }
            Action<string> handler = LineWritten;
            if (handler != null)
            {
                try { handler(line); } catch { }
            }
        }
    }
}
