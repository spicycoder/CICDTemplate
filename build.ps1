dotnet clean -c Release .\CICDTemplate.sln
dotnet restore .\CICDTemplate.sln
dotnet build -c Release .\CICDTemplate.sln
dotnet test -c Release .\CICDTemplate.sln --collect:"XPlat Code Coverage" --settings .\coverage.runsettings
dotnet reportgenerator "-reports:./**/coverage.cobertura.xml" "-targetdir:./.coverage" -reporttypes:"Html_Dark;SonarQube"
# dotnet stryker --solution .\CICDTemplate.sln -r "html" -r "progress" -r "markdown"