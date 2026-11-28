using System;
using System.Collections.Generic;
using ATM.Common;
using ATM.Server.Data;

namespace ATM.Server.Core
{
    /// <summary>
    /// [Rubric: App Logic] Tầng nghiệp vụ ("Bộ não" trong báo cáo):
    /// đăng ký / đăng nhập (khóa thẻ khi nhập sai PIN nhiều lần), rút / nộp / chuyển tiền,
    /// kiểm tra dữ liệu đầu vào, ghi lịch sử. Không phụ thuộc vào Socket hay giao diện.
    /// </summary>
    public sealed class AtmService
    {
        // Băm giả để việc đăng nhập thẻ không tồn tại cũng tốn thời gian như thẻ tồn tại (chống dò thẻ)
        private static readonly string DummyHash = CryptoUtil.HashPin("000000");

        private readonly IAtmRepository _repo;
        private readonly string _server;
        private readonly Action<string> _log;

        public AtmService(IAtmRepository repo, string serverName, Action<string> log)
        {
            _repo = repo;
            _server = serverName;
            _log = log ?? (m => { });
        }

        public Response Handle(Request r, SessionState s)
        {
            try
            {
                if (r == null || string.IsNullOrEmpty(r.Command)) return Response.Fail("Yêu cầu không hợp lệ.", "BAD_REQUEST");

                switch (r.Command)
                {
                    case Commands.Ping: return Response.Success("PONG");
                    case Commands.Register: return Register(r, s);
                    case Commands.Login: return Login(r, s);
                    case Commands.Logout: return Logout(s);
                }

                if (!s.IsLoggedIn) return Response.Fail("Vui lòng đăng nhập trước.", "AUTH");

                switch (r.Command)
                {
                    case Commands.Balance: return GetBalance(s);
                    case Commands.Withdraw: return Withdraw(r, s);
                    case Commands.Deposit: return Deposit(r, s);
                    case Commands.Transfer: return Transfer(r, s);
                    case Commands.Lookup: return Lookup(r);
                    case Commands.History: return History(r, s);
                    case Commands.ChangePin: return ChangePin(r, s);
                    default: return Response.Fail("Lệnh không được hỗ trợ: " + r.Command, "BAD_REQUEST");
                }
            }
            catch (Exception ex)
            {
                _log("[ERROR] " + r.Command + ": " + ex.Message);
                return Response.Fail("Lỗi máy chủ, vui lòng thử lại sau.", "SERVER_ERROR");
            }
        }

        // --------------------------------------------------------------- helpers

        private static bool IsDigits(string v, int min, int max)
        {
            if (v == null || v.Length < min || v.Length > max) return false;
            foreach (char c in v) if (c < '0' || c > '9') return false;
            return true;
        }

        private static string Now()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private static string CleanNote(string note, string fallback)
        {
            string n = (note ?? "").Trim();
            if (n.Length == 0) return fallback;
            return n.Length > 100 ? n.Substring(0, 100) : n;
        }

        private static string Mask(string card)
        {
            return card.Length > 4 ? new string('*', card.Length - 4) + card.Substring(card.Length - 4) : card;
        }

        private Response Ok(string msg, long balance)
        {
            var res = Response.Success(msg);
            res.Balance = balance;
            res.Time = Now();
            return res;
        }

        // ------------------------------------------------- đăng ký / đăng nhập

        private Response Register(Request r, SessionState s)
        {
            string name = (r.FullName ?? "").Trim();
            if (name.Length < 2 || name.Length > 50) return Response.Fail("Họ tên phải từ 2 đến 50 ký tự.", "VALIDATION");
            if (!IsDigits(r.Pin, Limits.PinLength, Limits.PinLength))
                return Response.Fail("Mã PIN phải gồm đúng " + Limits.PinLength + " chữ số.", "VALIDATION");

            string hash = CryptoUtil.HashPin(r.Pin);
            for (int i = 0; i < 10; i++)
            {
                string card = "10" + CryptoUtil.RandomDigits(8);
                if (_repo.TryCreateAccount(card, name, hash, 0, _server))
                {
                    _log("[REGISTER] Thẻ mới " + card + " (" + name + ") từ " + s.Remote);
                    var res = Response.Success("Đăng ký thành công!");
                    res.Card = card;
                    res.FullName = name;
                    res.Time = Now();
                    return res;
                }
            }
            return Response.Fail("Không tạo được số thẻ, vui lòng thử lại.", "SERVER_ERROR");
        }

        private Response Login(Request r, SessionState s)
        {
            if (!IsDigits(r.Card, 6, 16) || !IsDigits(r.Pin, Limits.PinLength, Limits.PinLength))
                return Response.Fail("Số thẻ hoặc mã PIN không đúng định dạng.", "VALIDATION");

            Account acc = _repo.FindAccount(r.Card);
            if (acc == null)
            {
                CryptoUtil.VerifyPin(r.Pin, DummyHash);
                _log("[LOGIN] Thất bại - thẻ không tồn tại: " + Mask(r.Card) + " từ " + s.Remote);
                return Response.Fail("Số thẻ hoặc mã PIN không đúng.", "AUTH");
            }

            if (acc.LockedUntil.HasValue && acc.LockedUntil.Value > DateTime.Now)
            {
                int secs = (int)Math.Ceiling((acc.LockedUntil.Value - DateTime.Now).TotalSeconds);
                return Response.Fail("Thẻ đang bị khóa tạm thời do nhập sai PIN. Thử lại sau " + secs + " giây.", "LOCKED");
            }

            if (!CryptoUtil.VerifyPin(r.Pin, acc.PinHash))
            {
                int failed = _repo.IncrementFailedAttempts(acc.CardNumber);
                if (failed >= Limits.MaxLoginAttempts)
                {
                    _repo.LockAccount(acc.CardNumber, DateTime.Now.AddSeconds(Limits.LockSeconds));
                    _log("[LOGIN] Khóa thẻ " + Mask(acc.CardNumber) + " " + Limits.LockSeconds + "s do sai PIN " + failed + " lần");
                    return Response.Fail("Sai PIN quá " + Limits.MaxLoginAttempts + " lần. Thẻ bị khóa " + Limits.LockSeconds + " giây.", "LOCKED");
                }
                _log("[LOGIN] Sai PIN thẻ " + Mask(acc.CardNumber) + " (lần " + failed + ")");
                return Response.Fail("Sai mã PIN. Bạn còn " + (Limits.MaxLoginAttempts - failed) + " lần thử.", "AUTH");
            }

            _repo.ClearFailedAttempts(acc.CardNumber);
            s.Card = acc.CardNumber;
            s.FullName = acc.FullName;
            _log("[LOGIN] Thành công thẻ " + Mask(acc.CardNumber) + " (" + acc.FullName + ") từ " + s.Remote);

            var res = Ok("Đăng nhập thành công", acc.Balance);
            res.Card = acc.CardNumber;
            res.FullName = acc.FullName;
            res.CreatedAt = acc.CreatedAt;
            return res;
        }

        private Response Logout(SessionState s)
        {
            if (s.IsLoggedIn) _log("[LOGOUT] Thẻ " + Mask(s.Card));
            s.Card = null;
            s.FullName = null;
            return Response.Success("Đã đăng xuất");
        }

        // ------------------------------------------------------------- tài khoản

        private Response GetBalance(SessionState s)
        {
            Account acc = _repo.FindAccount(s.Card);
            if (acc == null) return Response.Fail("Không tìm thấy tài khoản.", "NOT_FOUND");
            var res = Ok("OK", acc.Balance);
            res.Card = acc.CardNumber;
            res.FullName = acc.FullName;
            res.CreatedAt = acc.CreatedAt;
            return res;
        }

        private Response Lookup(Request r)
        {
            if (!IsDigits(r.ToCard, 6, 16)) return Response.Fail("Số thẻ không hợp lệ.", "VALIDATION");
            Account acc = _repo.FindAccount(r.ToCard);
            if (acc == null) return Response.Fail("Không tìm thấy số thẻ này.", "NOT_FOUND");
            var res = Response.Success("OK");
            res.Card = acc.CardNumber;
            res.FullName = acc.FullName;
            return res;
        }

        private Response History(Request r, SessionState s)
        {
            int count = r.Count <= 0 ? 50 : Math.Min(r.Count, 200);
            var res = Response.Success("OK");
            res.History = _repo.GetHistory(s.Card, count);
            return res;
        }

        private Response ChangePin(Request r, SessionState s)
        {
            if (!IsDigits(r.NewPin, Limits.PinLength, Limits.PinLength))
                return Response.Fail("PIN mới phải gồm đúng " + Limits.PinLength + " chữ số.", "VALIDATION");
            if (r.Pin == r.NewPin) return Response.Fail("PIN mới phải khác PIN hiện tại.", "VALIDATION");

            Account acc = _repo.FindAccount(s.Card);
            if (acc == null || !CryptoUtil.VerifyPin(r.Pin, acc.PinHash))
                return Response.Fail("PIN hiện tại không đúng.", "AUTH");

            _repo.UpdatePin(s.Card, CryptoUtil.HashPin(r.NewPin));
            _log("[PIN] Đổi PIN thẻ " + Mask(s.Card));
            return Ok("Đổi mã PIN thành công", acc.Balance);
        }

        // -------------------------------------------------------------- giao dịch

        private static string CheckAmount(long amount, long max)
        {
            if (amount <= 0) return "Số tiền phải lớn hơn 0.";
            if (amount % Limits.AmountStep != 0) return "Số tiền phải là bội số của " + Money.Format(Limits.AmountStep) + ".";
            if (amount > max) return "Số tiền tối đa mỗi giao dịch là " + Money.Format(max) + ".";
            return null;
        }

        private Response Withdraw(Request r, SessionState s)
        {
            string err = CheckAmount(r.Amount, Limits.MaxWithdraw);
            if (err != null) return Response.Fail(err, "VALIDATION");

            long balance;
            TxResult result = _repo.Withdraw(s.Card, r.Amount, CleanNote(r.Note, "Rút tiền tại ATM"), _server, out balance);
            if (result == TxResult.Insufficient) return Response.Fail("Số dư không đủ để thực hiện giao dịch.", "INSUFFICIENT");
            if (result == TxResult.NotFound) return Response.Fail("Không tìm thấy tài khoản.", "NOT_FOUND");

            _log("[WITHDRAW] " + Mask(s.Card) + " rút " + Money.Format(r.Amount));
            return Ok("Rút tiền thành công", balance);
        }

        private Response Deposit(Request r, SessionState s)
        {
            string err = CheckAmount(r.Amount, Limits.MaxDeposit);
            if (err != null) return Response.Fail(err, "VALIDATION");

            long balance;
            TxResult result = _repo.Deposit(s.Card, r.Amount, CleanNote(r.Note, "Nộp tiền tại ATM"), _server, out balance);
            if (result != TxResult.Ok) return Response.Fail("Không tìm thấy tài khoản.", "NOT_FOUND");

            _log("[DEPOSIT] " + Mask(s.Card) + " nộp " + Money.Format(r.Amount));
            return Ok("Nộp tiền thành công", balance);
        }

        private Response Transfer(Request r, SessionState s)
        {
            string err = CheckAmount(r.Amount, Limits.MaxTransfer);
            if (err != null) return Response.Fail(err, "VALIDATION");
            if (!IsDigits(r.ToCard, 6, 16)) return Response.Fail("Số thẻ nhận không hợp lệ.", "VALIDATION");
            if (r.ToCard == s.Card) return Response.Fail("Không thể chuyển khoản cho chính mình.", "VALIDATION");

            Account target = _repo.FindAccount(r.ToCard);
            if (target == null) return Response.Fail("Không tìm thấy thẻ người nhận.", "NOT_FOUND");

            long balance;
            TxResult result = _repo.Transfer(s.Card, r.ToCard, r.Amount, CleanNote(r.Note, "Chuyển khoản"), _server, out balance);
            if (result == TxResult.Insufficient) return Response.Fail("Số dư không đủ để thực hiện giao dịch.", "INSUFFICIENT");
            if (result == TxResult.NotFound) return Response.Fail("Không tìm thấy thẻ người nhận.", "NOT_FOUND");

            _log("[TRANSFER] " + Mask(s.Card) + " → " + Mask(r.ToCard) + " " + Money.Format(r.Amount));
            var res = Ok("Chuyển khoản thành công", balance);
            res.Card = target.CardNumber;
            res.FullName = target.FullName;
            return res;
        }
    }
}
