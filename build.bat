@echo Off
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

%nuget% pack -BasePath %SourcesPath%\Packs\ArmoLib.nuspec -NonInteractive -Version %version%
%nuget% pack -BasePath %SourcesPath%\Packs\HtmlDiff.nuspec -NonInteractive -Version %version%
%nuget% pack -BasePath %SourcesPath%\Packs\VariablesManager.nuspec -NonInteractive -Version %version%