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

set firstPartNuget=%nuget% pack %SourcesPath%\Packs\
set lastPartNuget=.nuspec -BasePath %SourcesPath% -NonInteractive -Version %version%

cmd /c %firstPartNuget%ArmoLib%lastPartNuget%
cmd /c %firstPartNuget%HtmlDiff%lastPartNuget%
cmd /c %firstPartNuget%VariablesManager%lastPartNuget%