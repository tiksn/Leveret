param(
    [Parameter(Mandatory=$true)] [string] $version
)

if ($IsWindows) {
    Invoke-psake -buildFile .\psake.ps1 -taskList "PublishChocolateyPackages" -properties @{"version"=$version}
}

if ($IsLinux) {
    Invoke-psake -buildFile .\psake.ps1 -taskList "PublishLinuxPackages" -properties @{"version"=$version}
}