cls

dotnet --version

dotnet restore src/Amplified.CSharp
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet build src/Amplified.CSharp
if %errorlevel% neq 0 exit /b %errorlevel%


dotnet restore src/Amplified.CSharp.Tests
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet build src/Amplified.CSharp.Tests
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet test src/Amplified.CSharp.Tests/Amplified.CSharp.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet pack src/Amplified.CSharp
if %errorlevel% neq 0 exit /b %errorlevel%