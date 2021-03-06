$ErrorActionPreference = "Stop"

$version = git describe --tags

if ($version.StartsWith("v")) {
    $version = $version.Substring(1)
}

Write-Output "Version: $version"

if ($version.Contains("-g"))
{
    [string]$confirmation = Read-Host "Continue? [y/N]"

    if (-Not ($confirmation -match "^[yY]$"))
    {
        Write-Output "Cancelled."
        Exit
    }
}

& dotnet pack .\src\BookFx\BookFx.csproj --configuration Release -p:Version=$version --output $pwd\.packs
& dotnet pack .\src\BookFx\BookFx.csproj --configuration Release -p:Version=$version --include-symbols -p:SymbolPackageFormat=snupkg --output $pwd\.packs
