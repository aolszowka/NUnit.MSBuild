REM Copyright (c) Ace Olszowka 2020. All rights reserved.
REM https://github.com/aolszowka/NUnit.MSBuild

REM Until we get this under CI here is a quick and dirty script to build this for deployment
nuget.exe pack ..\NUnit.MSBuild\NUnit.MSBuild.csproj -Properties Configuration=Release -Build -Tool
PAUSE
