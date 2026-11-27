@echo off
chcp 65001 >nul
setlocal
rem ============================================================
rem  Chay nhanh toan bo he thong demo:
rem    1 Load Balancer (cong 9000) + 2 ATM Server (9101, 9102) + 1 Client
rem  Yeu cau: da Build solution (Build ^> Build Solution trong Visual Studio) it nhat mot lan.
rem  Dung:  Chay-Demo.bat            (ban Debug)
rem         Chay-Demo.bat Release    (ban Release)
rem ============================================================
set "CFG=Debug"
if /i "%~1"=="Release" set "CFG=Release"
set "TFM=net8.0-windows"
set "ROOT=%~dp0"

set "LB=%ROOT%ATM.LoadBalancer\bin\%CFG%\%TFM%\ATM.LoadBalancer.exe"
set "SV=%ROOT%ATM.Server\bin\%CFG%\%TFM%\ATM.Server.exe"
set "CL=%ROOT%ATM.Client\bin\%CFG%\%TFM%\ATM.Client.exe"

for %%F in ("%LB%" "%SV%" "%CL%") do (
  if not exist %%F (
    echo [LOI] Khong tim thay %%F
    echo Hay mo ATM.sln trong Visual Studio va chon Build ^> Build Solution truoc.
    pause
    exit /b 1
  )
)

echo Dang khoi dong Load Balancer...
start "" "%LB%" --autostart
timeout /t 1 /nobreak >nul

echo Dang khoi dong ATM Server 1 va 2...
start "" "%SV%" --port 9101 --name Server-1 --autostart
start "" "%SV%" --port 9102 --name Server-2 --autostart
timeout /t 2 /nobreak >nul

echo Dang khoi dong Client...
start "" "%CL%"

echo.
echo Xong. Mo them Client bang cach chay lai file ATM.Client.exe.
timeout /t 3 >nul
