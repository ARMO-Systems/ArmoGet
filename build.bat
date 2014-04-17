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

echo %SourcesPath%

cmd /c %nuget% pack -BasePath %SourcesPath% %SourcesPath%\Packs\ArmoLib.nuspec -NonInteractive -Version %version%
cmd /c %nuget% pack -BasePath %SourcesPath% %SourcesPath%\Packs\HtmlDiff.nuspec -NonInteractive -Version %version%
cmd /c %nuget% pack -BasePath %SourcesPath% %SourcesPath%\Packs\VariablesManager.nuspec -NonInteractive -Version %version%