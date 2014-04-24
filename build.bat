@echo On
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Source\Main.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

cd Packs

set firstPartNuget=%nuget% pack 
set lastPartNuget= -NonInteractive %version%

cmd /c %firstPartNuget%ArmoLib.nuspec%lastPartNuget%
cmd /c %firstPartNuget%DeviceSettingsParser.nuspec%lastPartNuget%
cmd /c %firstPartNuget%GenerateTimexReferences.nuspec%lastPartNuget%
cmd /c %firstPartNuget%HaspClear.nuspec%lastPartNuget%
cmd /c %firstPartNuget%HtmlDiff.nuspec%lastPartNuget%
cmd /c %firstPartNuget%PullZKEmulation.nuspec%lastPartNuget%
cmd /c %firstPartNuget%ResourceToResEnum.nuspec%lastPartNuget%
cmd /c %firstPartNuget%VariablesManager.nuspec%lastPartNuget%
