$ErrorActionPreference = 'Stop';

$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$zipFile = if ((Get-OSArchitectureWidth 64) -and $env:chocolateyForceX86 -ne 'true') {
         Write-Host "Getting x64 bit zip"; Get-Item "$toolsDir\win7x64.zip"
} else { Write-Host "Getting x32 bit zip"; Get-Item "$toolsDir\win7x86.zip" }

Get-ChocolateyUnzip -FileFullPath $zipfile -Destination $toolsDir

# don't need zips anymore
Remove-Item ($toolsDir + '\*.' + 'zip')
