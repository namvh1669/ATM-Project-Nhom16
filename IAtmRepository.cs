using System;
using System.Collections.Generic;
using ATM.Common;

namespace ATM.Server.Data
{
    public class Account
    {
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public string PinHash { get; set; }
        public long Balance { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime? LockedUntil { get; set; }
        public string CreatedAt { get; set; }
    }

    public enum TxResult
    {
        Ok,
        NotFound,
        Insufficient
    }

    /// <summary>Tầng truy cập dữ liệu (Phần "Kho lưu trữ" trong báo cáo).</summary>
    public interface IAtmRepository
    {
        void Initialize();
        Account FindAccount(string card);
        bool TryCreateAccount(string card, string fullName, string pinHash, long initialBalance, string serverName);

        int IncrementFailedAttempts(string card);
        void LockAccount(string card, DateTime until);
        void ClearFailedAttempts(string card);
        void UpdatePin(string card, string newPinHash);

        TxResult Withdraw(string card, long amount, string note, string serverName, out long newBalance);
        TxResult Deposit(string card, long amount, string note, string serverName, out long newBalance);
        TxResult Transfer(string fromCard, string toCard, long amount, string note, string serverName, out long newBalance);

        List<TransactionInfo> GetHistory(string card, int count);
        List<Account> ListAccounts();
    }
}
