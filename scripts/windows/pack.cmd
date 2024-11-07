@echo off
if "%1" == "" (
	set "ProjectName=BootstrapBlazor"
)
set config=%2
if "%ProjectFolder%" == "" (
    set "ProjectFolder=%BB%"
)
if "%ProjectFolder%" == "" (
    echo Please set ProjectFolder evniroment Variables
    exit /B
)
if "%config%" == "" (
    set "config=Release"
)

cd %ProjectFolder%\%ProjectName%
dotnet pack -c %config% %ProjectFolder%\%ProjectName%\
copy %ProjectFolder%\%ProjectName%\bin\%config%\%ProjectName%*.nupkg %NugetLib% /y
del %ProjectFolder%\%ProjectName%\bin\%config%\%ProjectName%*.nupkg
cd %NugetLib%
set config=
echo Ready to DELETE %USERPROFILE%\.nuget\packages\%ProjectName% /S /F /Q
pause

del %USERPROFILE%\.nuget\packages\%ProjectName% /S /F /Q
dir %NugetLib%\%ProjectName%*.nupkg
