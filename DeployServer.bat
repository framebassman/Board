@echo off
chcp 1251
REM SET __MSBUILD_EXE__=%windir%\microsoft.net\framework\v4.0.30319\msbuild.exe

REM %__MSBUILD_EXE__% "%~pd0WebKeeper.sln" /M:1 /nodeReuse:False /t:Clean /verbosity:minimal 
REM %__MSBUILD_EXE__% "%~pd0WebKeeper.sln" /M:1 /nodeReuse:False /t:Build /verbosity:minimal 

cd /D C:\Program Files (x86)\IIS Express
@echo on
iisexpress /path:D:\Project\git-board\src\Board.Server /port:8482
cd /D D:\Project\git-board\src
