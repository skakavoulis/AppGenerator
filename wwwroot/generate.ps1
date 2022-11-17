param ($AppName = $(throw "App name is required."))

$ProjectPath = "$($pwd)\wwwroot\GeneratedCode\$AppName"
Write-Output $pwd
Write-Output $ProjectPath

dotnet new "console" -lang "C#" -n "$AppName" -o $ProjectPath --force

Compress-Archive -Path $ProjectPath -DestinationPath "$ProjectPath.zip" -Force -CompressionLevel Optimal
Write-Output "Compressed successfuly"
Remove-Item $ProjectPath -Recurse -Force
