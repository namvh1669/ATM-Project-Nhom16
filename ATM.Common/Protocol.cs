using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace ATM.Common
{
    /// <summary>Các lệnh Client gửi lên Server.</summary>
    public static class Commands
    {
        public const string Register = "REGISTER";
        public const string Login = "LOGIN";
        public const string Logout = "LOGOUT";
        public const string Balance = "BALANCE";
        public const string Withdraw = "WITHDRAW";
        public const string Deposit = "DEPOSIT";
        public const string Transfer = "TRANSFER";
        public const string Lookup = "LOOKUP";
        public const string History = "HISTORY";
        public const string ChangePin = "CHANGEPIN";
        public const string Ping = "PING";
    }

    public static class TxTypes
    {
        public const string Withdraw = "WITHDRAW";
        public const string Deposit = "DEPOSIT";
        public const string TransferOut = "TRANSFER_OUT";
        public const string TransferIn = "TRANSFER_IN";

        public static bool IsDebit(string type)
        {
            return type == Withdraw || type == TransferOut;
        }

        public static string Display(string type)
        {
            switch (type)
            {
                case Withdraw: return "Rút tiền";
                case Deposit: return "Nộp tiền";
                case TransferOut: return "Chuyển khoản đi";
                case TransferIn: return "Nhận chuyển khoản";
                default: return type;
            }
        }
    }

    /// <summary>Hằng số nghiệp vụ dùng chung cho Client và Server.</summary>
    public static class Limits
    {
        public const int PinLength = 6;
        public const int MaxLoginAttempts = 3;
        public const int LockSeconds = 60;
        public const long AmountStep = 10000;
        public const long MaxWithdraw = 10000000;
        public const long MaxDeposit = 100000000;
        public const long MaxTransfer = 100000000;
        public static readonly long[] QuickAmounts = { 100000, 200000, 500000, 1000000, 2000000, 5000000 };
    }

    public class Request
    {
        public string Command { get; set; }
        public string Card { get; set; }
        public string Pin { get; set; }
        public string NewPin { get; set; }
        public string FullName { get; set; }
        public string ToCard { get; set; }
        public long Amount { get; set; }
        public string Note { get; set; }
        public int Count { get; set; }
    }

    public class Response
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string ServerName { get; set; }
        public string Card { get; set; }
        public string FullName { get; set; }
        public long Balance { get; set; }
        public string CreatedAt { get; set; }
        public string Time { get; set; }
        public List<TransactionInfo> History { get; set; }

        public static Response Fail(string message, string code)
        {
            return new Response { Ok = false, Message = message, Code = code };
        }

        public static Response Success(string message)
        {
            return new Response { Ok = true, Message = message };
        }
    }

    public class TransactionInfo
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public long Amount { get; set; }
        public long BalanceAfter { get; set; }
        public string Note { get; set; }
        public string ServerName { get; set; }
        public string CreatedAt { get; set; }
    }

    public static class Json
    {
        public static byte[] ToBytes<T>(T value)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
        }

        public static T FromBytes<T>(byte[] data)
        {
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(data));
        }
    }

    public static class Money
    {
        /// <summary>1500000 -> "1.500.000 ₫"</summary>
        public static string Format(long amount)
        {
            return amount.ToString("#,0", CultureInfo.InvariantCulture).Replace(",", ".") + " ₫";
        }
    }
}
