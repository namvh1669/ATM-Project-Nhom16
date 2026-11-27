@echo off
chcp 65001 >nul
rem ============================================================
rem  Mo cong tuong lua Windows de may khac (LAN/Internet) ket noi toi he thong.
rem  PHAI chay bang "Run as administrator".
rem    Mo-Firewall.bat          : mo cong 9000 (Load Balancer) va 9101-9110 (ATM Server)
rem    Mo-Firewall.bat remove   : go bo cac quy tac da them
rem ============================================================
net session >nul 2>&1
if %errorlevel% neq 0 (
  echo [LOI] Hay chuot phai file nay va chon "Run as administrator".
  pause
  exit /b 1
)

if /i "%~1"=="remove" (
  netsh advfirewall firewall delete rule name="ATM Load Balancer" >nul
  netsh advfirewall firewall delete rule name="ATM Servers" >nul
  echo Da go bo quy tac tuong lua.
  pause
  exit /b 0
)

netsh advfirewall firewall delete rule name="ATM Load Balancer" >nul 2>&1
netsh advfirewall firewall delete rule name="ATM Servers" >nul 2>&1
netsh advfirewall firewall add rule name="ATM Load Balancer" dir=in action=allow protocol=TCP localport=9000 profile=any
netsh advfirewall firewall add rule name="ATM Servers" dir=in action=allow protocol=TCP localport=9101-9110 profile=any
echo.
echo Da mo cong TCP 9000 (Load Balancer) va 9101-9110 (ATM Server).
echo Khi demo xong, chay "Mo-Firewall.bat remove" de go bo.
pause
