param(
    [Parameter(Mandatory = $true)] [string] $version
)

if ($IsWindows) {
    Invoke-psake -buildFile .\psake.ps1 -taskList "PublishChocolateyPackage" -properties @{"version" = $version }
}

if ($IsLinux) {
    if ((Test-Path -Path "/usr/bin/dnf") -OR (Test-Path -Path "/usr/bin/yum")) {
        Invoke-psake -buildFile .\psake.ps1 -taskList "PublishLinuxRpmPackages" -properties @{"version" = $version }
    }
}