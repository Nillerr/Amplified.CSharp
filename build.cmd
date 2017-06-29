cls

dotnet --version

dotnet restore Amplified.CSharp
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet build Amplified.CSharp
if %errorlevel% neq 0 exit /b %errorlevel%


dotnet restore Amplified.CSharp.Tests
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet build Amplified.CSharp.Tests
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet test Amplified.CSharp.Tests/Amplified.CSharp.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet pack Amplified.CSharp
if %errorlevel% neq 0 exit /b %errorlevel%