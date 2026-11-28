using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ATM.Common;
using Microsoft.Data.Sqlite;

namespace ATM.Server.Data
{
    /// <summary>
    /// [Rubric: Database + Multi Server]
    /// Lưu tài khoản và lịch sử giao dịch bằng SQLite. Nhiều Server (nhiều tiến trình) dùng chung MỘT file .db:
    ///  - Bật chế độ WAL để nhiều tiến trình đọc/ghi đồng thời.
    ///  - Mỗi giao dịch tiền là một TRANSACTION, câu UPDATE kiểm tra số dư ngay trong điều kiện WHERE
    ///    nên không thể rút quá số dư dù hai Server xử lý cùng lúc.
    /// </summary>
    public sealed class SqliteAtmRepository : IAtmRepository
    {
        private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";
        private readonly string _connStr;

        public string DbPath { get; private set; }

        public SqliteAtmRepository(string dbPath)
        {
            DbPath = dbPath;
            string dir = Path.GetDirectoryName(Path.GetFullPath(dbPath));
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            _connStr = "Data Source=" + dbPath + ";Mode=ReadWriteCreate;Default Timeout=30";
        }

        private static string Now()
        {
            return DateTime.Now.ToString(TimeFormat, CultureInfo.InvariantCulture);
        }

        private SqliteConnection Open()
        {
            var con = new SqliteConnection(_connStr);
            con.Open();
            Run(con, null, "PRAGMA foreign_keys=ON;");
            return con;
        }

        private static SqliteCommand Make(SqliteConnection con, SqliteTransaction tx, string sql, object[] args)
        {
            SqliteCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            if (tx != null) cmd.Transaction = tx;
            for (int i = 0; i + 1 < args.Length; i += 2)
                cmd.Parameters.AddWithValue((string)args[i], args[i + 1] ?? DBNull.Value);
            return cmd;
        }

        /// <summary>Chạy câu lệnh không trả dữ liệu. args = tên, giá trị, tên, giá trị...</summary>
        private static int Run(SqliteConnection con, SqliteTransaction tx, string sql, params object[] args)
        {
            using (SqliteCommand cmd = Make(con, tx, sql, args)) return cmd.ExecuteNonQuery();
        }

        private static long ScalarLong(SqliteConnection con, SqliteTransaction tx, string sql, params object[] args)
        {
            using (SqliteCommand cmd = Make(con, tx, sql, args))
            {
                object o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0 : Convert.ToInt64(o);
            }
        }

        // ------------------------------------------------------------ khởi tạo

        public void Initialize()
        {
            using (SqliteConnection con = Open())
            {
                Run(con, null, "PRAGMA journal_mode=WAL;");
                Run(con, null,
                    "CREATE TABLE IF NOT EXISTS Accounts (" +
                    " CardNumber TEXT PRIMARY KEY," +
                    " FullName TEXT NOT NULL," +
                    " PinHash TEXT NOT NULL," +
                    " Balance INTEGER NOT NULL DEFAULT 0 CHECK (Balance >= 0)," +
                    " FailedAttempts INTEGER NOT NULL DEFAULT 0," +
                    " LockedUntil TEXT NULL," +
                    " CreatedAt TEXT NOT NULL);");
                Run(con, null,
                    "CREATE TABLE IF NOT EXISTS Transactions (" +
                    " Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    " CardNumber TEXT NOT NULL REFERENCES Accounts(CardNumber)," +
                    " Type TEXT NOT NULL," +
                    " Amount INTEGER NOT NULL," +
                    " BalanceAfter INTEGER NOT NULL," +
                    " Note TEXT NULL," +
                    " ServerName TEXT NULL," +
                    " CreatedAt TEXT NOT NULL);");
                Run(con, null, "CREATE INDEX IF NOT EXISTS IX_Tx_Card ON Transactions(CardNumber, Id DESC);");

                if (ScalarLong(con, null, "SELECT COUNT(*) FROM Accounts") == 0)
                {
                    Seed(con, "1000000001", "Nguyễn Văn An", "123456", 15000000);
                    Seed(con, "1000000002", "Trần Thị Bình", "123456", 8500000);
                    Seed(con, "1000000003", "Lê Minh Châu", "123456", 3200000);
                }
            }
        }

        private static void Seed(SqliteConnection con, string card, string name, string pin, long balance)
        {
            int n = Run(con, null,
                "INSERT OR IGNORE INTO Accounts (CardNumber, FullName, PinHash, Balance, CreatedAt) VALUES (@c, @n, @p, @b, @t)",
                "@c", card, "@n", name, "@p", CryptoUtil.HashPin(pin), "@b", balance, "@t", Now());
            if (n == 1 && balance > 0)
                Run(con, null,
                    "INSERT INTO Transactions (CardNumber, Type, Amount, BalanceAfter, Note, ServerName, CreatedAt) VALUES (@c, 'DEPOSIT', @a, @a, 'Số dư ban đầu', 'SEED', @t)",
                    "@c", card, "@a", balance, "@t", Now());
        }

        // -------------------------------------------------------------- tài khoản

        public Account FindAccount(string card)
        {
            using (SqliteConnection con = Open())
            using (SqliteCommand cmd = Make(con, null,
                "SELECT CardNumber, FullName, PinHash, Balance, FailedAttempts, LockedUntil, CreatedAt FROM Accounts WHERE CardNumber = @c",
                new object[] { "@c", card }))
            using (SqliteDataReader r = cmd.ExecuteReader())
            {
                if (!r.Read()) return null;
                return ReadAccount(r);
            }
        }

        private static Account ReadAccount(SqliteDataReader r)
        {
            var a = new Account();
            a.CardNumber = r.GetString(0);
            a.FullName = r.GetString(1);
            a.PinHash = r.GetString(2);
            a.Balance = r.GetInt64(3);
            a.FailedAttempts = (int)r.GetInt64(4);
            if (!r.IsDBNull(5))
            {
                DateTime t;
                if (DateTime.TryParseExact(r.GetString(5), TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out t))
                    a.LockedUntil = t;
            }
            a.CreatedAt = r.GetString(6);
            return a;
        }

        public List<Account> ListAccounts()
        {
            var list = new List<Account>();
            using (SqliteConnection con = Open())
            using (SqliteCommand cmd = Make(con, null,
                "SELECT CardNumber, FullName, PinHash, Balance, FailedAttempts, LockedUntil, CreatedAt FROM Accounts ORDER BY CreatedAt, CardNumber",
                new object[0]))
            using (SqliteDataReader r = cmd.ExecuteReader())
            {
                while (r.Read()) list.Add(ReadAccount(r));
            }
            return list;
        }

        public bool TryCreateAccount(string card, string fullName, string pinHash, long initialBalance, string serverName)
        {
            using (SqliteConnection con = Open())
            using (SqliteTransaction tx = con.BeginTransaction())
            {
                try
                {
                    Run(con, tx,
                        "INSERT INTO Accounts (CardNumber, FullName, PinHash, Balance, CreatedAt) VALUES (@c, @n, @p, @b, @t)",
                        "@c", card, "@n", fullName, "@p", pinHash, "@b", initialBalance, "@t", Now());
                    tx.Commit();
                    return true;
                }
                catch (SqliteException ex)
                {
                    if (ex.SqliteErrorCode == 19) return false; // SQLITE_CONSTRAINT: trùng số thẻ
                    throw;
                }
            }
        }

        public int IncrementFailedAttempts(string card)
        {
            using (SqliteConnection con = Open())
            using (SqliteTransaction tx = con.BeginTransaction())
            {
                Run(con, tx, "UPDATE Accounts SET FailedAttempts = FailedAttempts + 1 WHERE CardNumber = @c", "@c", card);
                long n = ScalarLong(con, tx, "SELECT FailedAttempts FROM Accounts WHERE CardNumber = @c", "@c", card);
                tx.Commit();
                return (int)n;
            }
        }

        public void LockAccount(string card, DateTime until)
        {
            using (SqliteConnection con = Open())
                Run(con, null, "UPDATE Accounts SET FailedAttempts = 0, LockedUntil = @u WHERE CardNumber = @c",
                    "@u", until.ToString(TimeFormat, CultureInfo.InvariantCulture), "@c", card);
        }

        public void ClearFailedAttempts(string card)
        {
            using (SqliteConnection con = Open())
                Run(con, null, "UPDATE Accounts SET FailedAttempts = 0, LockedUntil = NULL WHERE CardNumber = @c AND (FailedAttempts <> 0 OR LockedUntil IS NOT NULL)", "@c", card);
        }

        public void UpdatePin(string card, string newPinHash)
        {
            using (SqliteConnection con = Open())
                Run(con, null, "UPDATE Accounts SET PinHash = @p WHERE CardNumber = @c", "@p", newPinHash, "@c", card);
        }

        // -------------------------------------------------------------- giao dịch

        private static void InsertTx(SqliteConnection con, SqliteTransaction tx, string card, string type, long amount, long balanceAfter, string note, string server)
        {
            Run(con, tx,
                "INSERT INTO Transactions (CardNumber, Type, Amount, BalanceAfter, Note, ServerName, CreatedAt) VALUES (@c, @t, @a, @b, @n, @s, @d)",
                "@c", card, "@t", type, "@a", amount, "@b", balanceAfter, "@n", note, "@s", server, "@d", Now());
        }

        public TxResult Withdraw(string card, long amount, string note, string serverName, out long newBalance)
        {
            newBalance = 0;
            using (SqliteConnection con = Open())
            using (SqliteTransaction tx = con.BeginTransaction())
            {
                // Trừ tiền nguyên tử: chỉ thành công khi số dư >= số tiền rút
                int n = Run(con, tx, "UPDATE Accounts SET Balance = Balance - @a WHERE CardNumber = @c AND Balance >= @a",
                    "@a", amount, "@c", card);
                if (n == 0)
                {
                    bool exists = ScalarLong(con, tx, "SELECT COUNT(*) FROM Accounts WHERE CardNumber = @c", "@c", card) > 0;
                    return exists ? TxResult.Insufficient : TxResult.NotFound; // thoát using => rollback
                }
                newBalance = ScalarLong(con, tx, "SELECT Balance FROM Accounts WHERE CardNumber = @c", "@c", card);
                InsertTx(con, tx, card, TxTypes.Withdraw, amount, newBalance, note, serverName);
                tx.Commit();
                return TxResult.Ok;
            }
        }

        public TxResult Deposit(string card, long amount, string note, string serverName, out long newBalance)
        {
            newBalance = 0;
            using (SqliteConnection con = Open())
            using (SqliteTransaction tx = con.BeginTransaction())
            {
                int n = Run(con, tx, "UPDATE Accounts SET Balance = Balance + @a WHERE CardNumber = @c", "@a", amount, "@c", card);
                if (n == 0) return TxResult.NotFound;
                newBalance = ScalarLong(con, tx, "SELECT Balance FROM Accounts WHERE CardNumber = @c", "@c", card);
                InsertTx(con, tx, card, TxTypes.Deposit, amount, newBalance, note, serverName);
                tx.Commit();
                return TxResult.Ok;
            }
        }

        public TxResult Transfer(string fromCard, string toCard, long amount, string note, string serverName, out long newBalance)
        {
            newBalance = 0;
            using (SqliteConnection con = Open())
            using (SqliteTransaction tx = con.BeginTransaction())
            {
                int n = Run(con, tx, "UPDATE Accounts SET Balance = Balance - @a WHERE CardNumber = @c AND Balance >= @a",
                    "@a", amount, "@c", fromCard);
                if (n == 0) return TxResult.Insufficient;

                int m = Run(con, tx, "UPDATE Accounts SET Balance = Balance + @a WHERE CardNumber = @c", "@a", amount, "@c", toCard);
                if (m == 0) return TxResult.NotFound; // rollback cả lệnh trừ tiền ở trên

                newBalance = ScalarLong(con, tx, "SELECT Balance FROM Accounts WHERE CardNumber = @c", "@c", fromCard);
                long toBalance = ScalarLong(con, tx, "SELECT Balance FROM Accounts WHERE CardNumber = @c", "@c", toCard);
                InsertTx(con, tx, fromCard, TxTypes.TransferOut, amount, newBalance, note + " → " + toCard, serverName);
                InsertTx(con, tx, toCard, TxTypes.TransferIn, amount, toBalance, note + " ← " + fromCard, serverName);
                tx.Commit();
                return TxResult.Ok;
            }
        }

        public List<TransactionInfo> GetHistory(string card, int count)
        {
            var list = new List<TransactionInfo>();
            using (SqliteConnection con = Open())
            using (SqliteCommand cmd = Make(con, null,
                "SELECT Id, Type, Amount, BalanceAfter, Note, ServerName, CreatedAt FROM Transactions WHERE CardNumber = @c ORDER BY Id DESC LIMIT @n",
                new object[] { "@c", card, "@n", count }))
            using (SqliteDataReader r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var t = new TransactionInfo();
                    t.Id = r.GetInt64(0);
                    t.Type = r.GetString(1);
                    t.Amount = r.GetInt64(2);
                    t.BalanceAfter = r.GetInt64(3);
                    t.Note = r.IsDBNull(4) ? "" : r.GetString(4);
                    t.ServerName = r.IsDBNull(5) ? "" : r.GetString(5);
                    t.CreatedAt = r.GetString(6);
                    list.Add(t);
                }
            }
            return list;
        }
    }
}
