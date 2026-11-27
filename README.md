# ATM Banking – Đồ án Lập trình mạng căn bản

Phần mềm quản lý và giao dịch ATM theo mô hình **Client – Load Balancer – nhiều Server**, viết bằng **C# WinForms (.NET 8)**, chạy trên **Visual Studio 2022**. Giao diện tối kiểu ngân hàng số, toàn bộ dữ liệu truyền qua mạng đều được mã hóa.

```
                         ┌────────────────────┐
  ┌─────────┐   TCP      │                    │    TCP    ┌──────────────┐
  │ Client 1│───────────▶│                    │──────────▶│ ATM Server 1 │──┐
  └─────────┘            │   Load Balancer    │           └──────────────┘  │   ┌───────────┐
  ┌─────────┐   (LAN /   │  (cổng 9000)       │           ┌──────────────┐  ├──▶│ SQLite DB │
  │ Client 2│───Internet)│  Round-Robin /     │──────────▶│ ATM Server 2 │──┤   │ (dùng     │
  └─────────┘───────────▶│  Least-Connections │           └──────────────┘  │   │  chung)   │
  ┌─────────┐            │  Health-check      │           ┌──────────────┐  │   └───────────┘
  │ Client N│───────────▶│  Failover          │──────────▶│ ATM Server N │──┘
  └─────────┘            └────────────────────┘           └──────────────┘
        └────────── kênh mã hóa đầu cuối RSA-2048 + AES-256 + HMAC (LB chỉ chuyển tiếp byte) ──────────┘
```

---

## 1. Đối chiếu với thang điểm của giáo viên

| Tiêu chí | Điểm | Thực hiện trong dự án | File chính |
|---|---|---|---|
| **App Logic + Socket Logic** | 5 | Đăng ký / đăng nhập, rút nhanh, rút tùy chọn, nộp tiền, **chuyển khoản**, xem số dư, lịch sử, đổi PIN. Giao thức riêng trên TCP (khung độ dài + JSON), khóa thẻ khi sai PIN 3 lần, kiểm tra hạn mức, giao dịch nguyên tử (không rút quá số dư dù 2 Server xử lý cùng lúc). | `ATM.Server/Core/AtmService.cs`, `ServerHost.cs`, `ATM.Common/Protocol.cs` |
| **I/O (File, Network…)** | 0,5 | Network: `NetworkStream` + khung dữ liệu. File: nhật ký `logs\*.log`, cấu hình `client.config.json` / `lb.config.json`, **xuất sao kê CSV**. | `FileLogger.cs`, `ClientConfig.cs`, `LbConfig.cs`, `InfoPages.cs` |
| **Database** | 0,5 | SQLite (bảng `Accounts`, `Transactions`), TRANSACTION, WAL. Có thể xem trực tiếp ở tab "Tài khoản & số dư" của Server. | `ATM.Server/Data/SqliteAtmRepository.cs` |
| **Thread** | 0,5 | Server: 1 luồng Accept + **1 luồng/Client**. LB: luồng Accept, luồng Health-check, 2 luồng Pump/kết nối. Client: luồng Heartbeat, luồng nạp Splash, `async/await` giữ UI không bị treo. | `ServerHost.cs`, `BalancerService.cs`, `AtmClient.cs`, `SplashForm.cs` |
| **Sign up / Sign in** | 0,5 | Đăng ký (Server tự cấp số thẻ, PIN băm PBKDF2), đăng nhập, lưu trạng thái phiên theo kết nối, nhớ số thẻ lần trước. | `RegisterPage.cs`, `LoginPage.cs`, `AtmService.cs` |
| **Multi Client** | 0,5 | Nhiều Client đăng nhập đồng thời, cùng hoặc khác thẻ. Tab "Client đang kết nối" ở Server hiển thị danh sách. | `ServerHost.cs` |
| **Multi Server** | 0,5 | Chạy nhiều `ATM.Server.exe` (cổng 9101, 9102…) dùng chung một CSDL; tự chọn cổng trống. | `ServerForm.cs` |
| **Cryptography** | 0,5 | Bắt tay **RSA-2048 (OAEP-SHA256)** → khóa phiên; dữ liệu **AES-256-CBC + HMAC-SHA256** (encrypt-then-MAC, số thứ tự chống phát lại); PIN lưu bằng **PBKDF2-SHA256 + salt**; hiển thị vân tay khóa Server. | `SecureChannel.cs`, `CryptoUtil.cs` |
| **Demo via LAN** | 0,5 | Client nhập IP của máy chạy LB trong hộp thoại **Kết nối** (có nút Kiểm tra). LB hiển thị sẵn IP LAN của máy. | `ConnectionDialog.cs`, `LbForm.cs` |
| **Demo via Internet** | 0,5 | Client nhận cả tên miền / địa chỉ tunnel. Xem mục 5. | `ConnectionDialog.cs` |
| **Load Balancing** | 1 | TCP reverse-proxy, **Round-Robin** và **Least-Connections** (đổi nóng trên giao diện), **health-check** 2 giây, **failover** khi Server chết, bảng theo dõi tải từng Server. | `ATM.LoadBalancer/Core/BalancerService.cs` |

---

## 2. Yêu cầu và cách chạy trên Visual Studio

**Cần có:** Visual Studio 2022 (17.8 trở lên) với workload **".NET desktop development"**, .NET 8 SDK (đi kèm VS). Lần build đầu cần Internet để NuGet tải `Microsoft.Data.Sqlite`.

### Cách A – nhanh nhất
1. Mở `ATM.sln` → **Build → Build Solution** (Ctrl+Shift+B).
2. Chạy file **`Chay-Demo.bat`**: tự mở 1 Load Balancer + 2 ATM Server + 1 Client.
3. Muốn thêm Client: chạy lại `ATM.Client\bin\Debug\net8.0-windows\ATM.Client.exe`.

### Cách B – chạy từng phần trong Visual Studio
1. Chuột phải project **ATM.Server** → *Debug → Start New Instance* (làm **hai lần** để có hai Server; chương trình tự chọn cổng 9101, 9102 và tên Server-1, Server-2). Nhấn **Khởi động** ở mỗi cửa sổ.
2. Chuột phải **ATM.LoadBalancer** → *Debug → Start New Instance* → nhấn **Khởi động** (cổng 9000; danh sách backend mặc định đã trỏ tới 127.0.0.1:9101 và :9102).
3. Chuột phải **ATM.Client** → *Debug → Start New Instance*. Có thể mở nhiều Client.

> Lần đầu Windows hỏi quyền tường lửa → chọn **Allow access**.
>
> **Thứ tự khởi động: ATM Server → Load Balancer → Client.** Nhấn F5 riêng project *ATM.Client* khi chưa chạy Load Balancer / Server sẽ báo không kết nối được (đúng như thiết kế) - hãy khởi động hai phần kia trước hoặc dùng `Chay-Demo.bat`.

### Tài khoản demo (tạo sẵn khi Server khởi động lần đầu)
| Số thẻ | PIN | Chủ thẻ | Số dư |
|---|---|---|---|
| 1000000001 | 123456 | Nguyễn Văn An | 15.000.000 ₫ |
| 1000000002 | 123456 | Trần Thị Bình | 8.500.000 ₫ |
| 1000000003 | 123456 | Lê Minh Châu | 3.200.000 ₫ |

Ở màn hình đăng nhập có nút **"Điền nhanh tài khoản demo"**. Cơ sở dữ liệu nằm ở `C:\ProgramData\ATMDemo\atm.db` (xóa file này để reset về trạng thái ban đầu).

---

## 3. Kịch bản demo gợi ý (khoảng 5 phút)

1. **Multi Server + Load Balancing:** mở LB và 2 Server. Mở 4 Client → trên LB thấy bảng "Đã phục vụ" chia 2/2; mỗi Client hiển thị "Đã kết nối • Server-x" ở góc phải trên.
2. **Multi Client + Database:** Client A (thẻ …0001) chuyển khoản cho thẻ …0002; Client B đăng nhập thẻ …0002 → thấy tiền về, lịch sử có dòng "Nhận chuyển khoản". Mở tab *Tài khoản & số dư* của Server để thấy số dư trong DB.
3. **Sign up:** Đăng ký thẻ mới → nhận số thẻ 10 chữ số → đăng nhập → nộp tiền.
4. **Bảo mật:** nhập sai PIN 3 lần → thẻ bị khóa 60 giây. Chỉ vào tooltip của nhãn "Đã kết nối" để show vân tay khóa RSA.
5. **Failover:** tắt Server mà Client đang dùng → Client báo "Mất kết nối" và quay về đăng nhập; đăng nhập lại → LB tự chuyển sang Server còn sống (LB đánh dấu DOWN màu đỏ). Bật lại Server → LB đánh dấu UP.
6. **I/O File:** vào *Lịch sử giao dịch* → **Xuất sao kê (CSV)**; mở thư mục `logs` cạnh file `.exe` của Server/LB để show nhật ký.
7. **Least-Connections:** trên LB bấm đổi thuật toán, mở thêm Client để thấy kết nối dồn về Server đang ít việc hơn.

---

## 4. Demo qua mạng LAN

1. Máy **chủ** (chạy LB + các Server): chạy **`Mo-Firewall.bat` bằng quyền Administrator** (mở cổng 9000 và 9101-9110).
2. Cửa sổ Load Balancer hiển thị dòng *"IP của máy này (nhập vào Client khi demo LAN): 192.168.x.x"*.
3. Máy **khách**: chạy `ATM.Client.exe` (copy cả thư mục `bin\Debug\net8.0-windows`; máy khách cần .NET 8 Desktop Runtime) → nút **Kết nối** → nhập IP trên, cổng 9000 → **Kiểm tra** → **Lưu**.
4. Hai máy phải cùng mạng Wi-Fi/LAN. Nếu "Kiểm tra" báo lỗi: kiểm tra tường lửa, và đừng dùng mạng khách (guest) có chặn thiết bị với nhau.

> Có thể chạy Server ở **máy khác** với LB: trong LB nhập IP máy đó + cổng Server. Lưu ý các Server phải dùng chung cơ sở dữ liệu (xem mục 7).

## 5. Demo qua Internet

Chỉ cần **một cổng (9000 của Load Balancer)** được truy cập từ ngoài. Chọn một cách:

| Cách | Thực hiện | Ghi chú |
|---|---|---|
| **Tunnel (dễ nhất, không cần sửa router)** | Dùng *ngrok* (`ngrok tcp 9000`) hoặc *playit.gg*. Dịch vụ cấp địa chỉ dạng `0.tcp.ngrok.io:12345`. | Nhập đúng host và cổng đó vào Client. Nhà mạng dùng CGNAT thì cách này là lựa chọn phù hợp. |
| **Port-forward trên router** | Router → chuyển tiếp TCP cổng 9000 tới IP LAN của máy chạy LB. Tra IP công cộng (ví dụ bằng trang `whatismyip`). | Nhập IP công cộng vào Client. Cần có IP công cộng thật, không bị CGNAT. |
| **VPN mesh (Tailscale / ZeroTier)** | Cài trên cả hai máy, dùng IP ảo của máy chủ. | Không cần mở cổng router, ổn định khi demo. |
| **VPS** | Chạy LB + Server trên một máy chủ ảo, mở cổng 9000. | Dùng IP/tên miền của VPS. |

Phần mềm hoạt động giống hệt nhau ở LAN và Internet vì Client chỉ cần biết địa chỉ Load Balancer; **dữ liệu đã được mã hóa đầu cuối** nên đi qua Internet vẫn an toàn.

---

## 6. Giao thức và bảo mật (để trả lời khi bị hỏi)

1. **Khung dữ liệu (frame):** `[4 byte độ dài][dữ liệu]`. Giải quyết bài toán "TCP là luồng byte, không có ranh giới gói".
2. **Bắt tay:** Server gửi khóa công khai RSA-2048 (tạo khi khởi động). Client sinh 64 byte ngẫu nhiên (khóa AES-256 + khóa HMAC), mã hóa bằng **RSA-OAEP** và gửi lên.
3. **Mỗi gói tin:** `IV(16) | AES-256-CBC(JSON) | HMAC-SHA256(hướng‖số thứ tự‖IV‖ciphertext)`. Kiểm tra MAC trước khi giải mã; số thứ tự + hướng gửi chặn tấn công phát lại / phản xạ. Gói bị sửa → Server ngắt kết nối và ghi cảnh báo.
4. **Load Balancer** chỉ chuyển tiếp byte nên **không đọc được** nội dung.
5. **Mã PIN:** gửi qua kênh đã mã hóa, Server lưu `PBKDF2-SHA256(100.000 vòng) + salt ngẫu nhiên`, so sánh bằng hàm thời gian hằng số. Đăng nhập thẻ không tồn tại cũng tốn thời gian như thẻ có thật (chống dò thẻ).
6. **Tính đúng đắn khi nhiều Server:** lệnh trừ tiền là một câu `UPDATE … WHERE Balance >= @amount` trong TRANSACTION; hai Server cùng rút một thẻ cũng không thể làm số dư âm.

**Hạn chế đã biết (nên chủ động nêu):** chưa dùng chứng chỉ số nên Client chỉ hiển thị vân tay khóa (không tự đối chiếu) – chống nghe lén nhưng chưa chống kẻ đứng giữa chủ động; hướng nâng cấp là "ghim" vân tay hoặc dùng TLS.

## 7. Cấu trúc mã nguồn và làm việc với Designer

```
ATM.sln
├─ ATM.Common/         Giao thức (Protocol), mã hóa (SecureChannel, CryptoUtil), ghi log (FileLogger)
├─ ATM.UI/             Theme + control tự vẽ dùng chung (xuất hiện trong Toolbox), hộp thoại thông báo
├─ ATM.Server/         Data (SQLite) · Core (AtmService, ServerHost) · ServerForm (+ .Designer.cs)
├─ ATM.LoadBalancer/   Core (BalancerService) · LbForm (+ .Designer.cs)
├─ ATM.Client/         Net (AtmClient) · Forms (Splash, Main, Kết nối) · Pages (9 màn hình) – đều có .Designer.cs
├─ Chay-Demo.bat       Chạy nhanh LB + 2 Server + Client
└─ Mo-Firewall.bat     Mở/đóng cổng tường lửa khi demo LAN/Internet
```

**Giao diện dựng bằng Designer.** Mỗi form / trang gồm hai file: `X.cs` (xử lý sự kiện, nghiệp vụ) và `X.Designer.cs` (vùng `InitializeComponent` do Designer quản lý). Ví dụ: bấm đúp `LoginPage.cs` trong Solution Explorer để mở màn hình thiết kế.

| Cần sửa | Mở |
|---|---|
| Đăng nhập / Đăng ký / Màn hình chính | `ATM.Client\Pages\LoginPage.cs`, `RegisterPage.cs`, `HomePage.cs` |
| Rút nhanh / Rút-Nộp tiền / Chuyển khoản | `QuickWithdrawPage.cs`, `AmountPage.cs`, `TransferPage.cs` |
| Số dư / Lịch sử / Đổi PIN | `BalancePage.cs`, `HistoryPage.cs`, `ChangePinPage.cs` |
| Cửa sổ chính (thanh trên cùng), Splash, hộp thoại Kết nối | `ATM.Client\Forms\MainForm.cs`, `SplashForm.cs`, `ConnectionDialog.cs` |
| Bảng điều khiển Server / Load Balancer | `ATM.Server\ServerForm.cs`, `ATM.LoadBalancer\LbForm.cs` |

Cách làm việc:
1. **Build trước** (Ctrl+Shift+B) rồi mới mở Designer, để Visual Studio nạp các control tùy biến.
2. Trong **Toolbox** sẽ có nhóm *ATM.UI Components*: `RoundedPanel`, `RoundedButton`, `ModernTextBox`, `PinDots`, `NumPad`, `StatCard`, `StatusPill`, `ThemedGrid`, `PagePanel`, `PageHeader`… Kéo thả và chỉnh trong **Properties** (các thuộc tính riêng nằm ở nhóm **ATM**, ví dụ `Kind`, `Icon`, `Caption`, `NumbersOnly`, `MaxContentWidth`).
3. Mỗi trang đặt `PageHeader` (Dock Top) + `PagePanel` (Dock Fill). `PagePanel.MaxContentWidth` giới hạn bề rộng nội dung khi cửa sổ rộng; `MinContentHeight` bật thanh cuộn khi cửa sổ thấp. Đặt `MaxContentWidth` của hai control giống nhau để thẳng hàng.
4. Dùng bảng (`TableLayoutPanel`) và `Dock`/`Anchor` thay vì đặt tọa độ tuyệt đối để giao diện co giãn theo cửa sổ và DPI.
5. Đừng sửa tay trong vùng *Windows Form Designer generated code*; viết xử lý sự kiện (Click, TextChanged…) trong file `X.cs`.

**Phần vẫn giữ bằng code (do Designer không biểu diễn được):**
- Các control tự vẽ bằng GDI+: nút bo góc, ô nhập, bàn phím số, ô PIN, thẻ thống kê (`ATM.UI\Controls.cs`), biểu tượng (`Icons.cs`), thẻ số dư `BalanceCard`, ô chức năng `TileButton`, khung giới thiệu `BrandPanel` (`ATM.Client\Pages`). Chúng vẫn kéo thả được trong Designer như các control thường.
- Hộp thoại thông báo / xác nhận / biên lai (`ATM.UI\Dialogs.cs`): bố cục tính theo độ dài nội dung nên dựng bằng code.
- Màu sắc, phông chữ mặc định, hàm vẽ bo góc: `ATM.UI\Theme.cs`. Trong các file Designer, màu/phông của nhãn và panel được ghi dạng giá trị cụ thể (ví dụ `Color.FromArgb(241, 245, 249)`); nếu đổi bảng màu toàn bộ thì sửa `Theme.cs` và các giá trị tương ứng trong Properties.
- Co giãn DPI cho các trang nạp lúc chạy: `MainForm.Navigate`.

**Dữ liệu dùng chung giữa các Server:** bản này dùng một file SQLite nên các Server cần chạy trên **cùng một máy** (nhiều tiến trình dùng chung file `atm.db`, đã bật chế độ WAL). Muốn tách Server sang máy khác, chỉ cần viết thêm một lớp triển khai `IAtmRepository` cho SQL Server / MySQL – phần nghiệp vụ và mạng không phải sửa.

## 8. Xử lý sự cố

| Hiện tượng | Cách xử lý |
|---|---|
| Visual Studio hiện cửa sổ *Exception User-Unhandled* khi đang debug | Nếu còn thấy: nhấn **Continue (F5)** - ứng dụng sẽ hiện hộp thoại lỗi bình thường (hoặc chạy bằng Ctrl+F5). Bản hiện tại đã xử lý các lỗi kết nối nên thường không còn bị dừng lại. |
| Client báo "Không kết nối được máy chủ" | Chưa Khởi động LB / Server; sai IP; tường lửa (chạy `Mo-Firewall.bat`). Dùng nút **Kiểm tra** trong hộp thoại Kết nối để biết lý do. |
| LB hiện Server màu đỏ **DOWN** | Server đó chưa Khởi động hoặc khác cổng so với danh sách backend. |
| "Không mở được cổng …" | Cổng đã bị chương trình khác dùng – đổi cổng hoặc tắt tiến trình cũ. |
| Build lỗi không tìm thấy `Microsoft.Data.Sqlite` | Cần Internet cho lần khôi phục NuGet đầu tiên (*Tools → NuGet Package Manager → Restore*). |
| Chữ/ô bị nhỏ trên màn hình độ phân giải cao | Ứng dụng đã hỗ trợ DPI; nếu cửa sổ lớn hơn màn hình, nội dung tự có thanh cuộn. |
| Muốn xóa dữ liệu làm lại | Đóng các Server, xóa `C:\ProgramData\ATMDemo\atm.db*`. |
