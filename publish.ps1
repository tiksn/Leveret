
if ($IsWindows) {
    Invoke-psake -buildFile .\psake.ps1 -taskList "PublishChocolateyPackages"
}

if ($IsLinux) {
    Invoke-psake -buildFile .\psake.ps1 -taskList "PublishLinuxPackages"
}