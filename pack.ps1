$ErrorActionPreference = "Stop"

& dotnet pack .\src\BookFx\BookFx.csproj --configuration Release --output $pwd\.packs
& dotnet pack .\src\BookFx\BookFx.csproj --configuration Release --include-symbols -p:SymbolPackageFormat=snupkg --output $pwd\.packs
