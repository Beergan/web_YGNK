@ECHO OFF
rem set CurrentDir="%~dp0"
rem echo %CurrentDir%
for %%I in (..) do set ParentFolderName=%%~nI%%~xI
rem echo %ParentFolderName%
if "%ParentFolderName%" NEQ "Package" goto end
del *.ashx
del packages.config
FOR /d /r . %%d IN ("roslyn") DO @IF EXIST "%%d" rmdir /s /q "%%d"
del *.cshtml /S /Q
for /f "delims=" %%d in ('dir /s /b /ad ^| sort /r') do rd "%%d"
powershell -Command "(gc web.config) -replace '<compilation debug=""true', '<compilation debug=""""false' | Out-File -Encoding UTF8 web.config"
rmdir /s /q log
start /b del *.cmd
:end
PAUSE