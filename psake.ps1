Properties {
    $version = '0.0.1'
}

Task PublishChocolateyPackage -depends PackChocolateyPackage {
    Exec { choco push $script:chocoNupkg }
}

Task PublishLinuxRpmPackages -depends PackLinuxPackage {
    #
}

Task PackLinuxPackage -depends BuildLinux64 {
}

Task PackChocolateyPackage -depends ZipBuildArtifacts {
    Copy-Item -Path .\Chocolatey\tools\chocolateyInstall.ps1 -Destination $script:chocoTools
    Copy-Item -Path .\Chocolatey\tools\LICENSE.txt -Destination $script:chocoLegal
    Copy-Item -Path .\Chocolatey\tools\VERIFICATION.txt -Destination $script:chocoLegal

    $verificationFilePath = Join-Path -Path $script:chocoLegal -ChildPath VERIFICATION.txt

    $zipX64Hash = Get-FileHash -Path $script:artifactsZipX64
    $zipX86Hash = Get-FileHash -Path $script:artifactsZipX86

    Add-Content -Path $verificationFilePath -Value ('File Hash: winx64.zip - ' + $zipX64Hash.Algorithm + ' - ' + $zipX64Hash.Hash)
    Add-Content -Path $verificationFilePath -Value ('File Hash: winx86.zip - ' + $zipX86Hash.Algorithm + ' - ' + $zipX86Hash.Hash)

    $chocoNuspec = Join-Path -Path $script:chocolateyPublishFolderFolder -ChildPath leveret.nuspec
    Copy-Item -Path '.\Chocolatey\leveret.nuspec' -Destination $chocoNuspec
    Exec { choco pack $chocoNuspec --version $version --outputdirectory $script:trashFolder version=$version }
    $script:chocoNupkg = Join-Path -Path $script:trashFolder -ChildPath "Leveret.$version.nupkg"
}

Task ZipBuildArtifacts -depends BuildWinX64, BuildWinX86 {
    $script:artifactsZipX64 = Join-Path -Path $script:chocoTools -ChildPath 'winx64.zip'
    $script:artifactsZipX86 = Join-Path -Path $script:chocoTools -ChildPath 'winx86.zip'

    Compress-Archive -Path "$script:publishWinX64Folder\*" -CompressionLevel Optimal -DestinationPath $script:artifactsZipX64
    Compress-Archive -Path "$script:publishWinX86Folder\*" -CompressionLevel Optimal -DestinationPath $script:artifactsZipX86
}

Task Build -depends BuildWinX64, BuildWinX86, BuildLinux64

Task BuildWinX64 -depends PreBuild {
    $script:publishWinX64Folder = Join-Path -Path $script:publishFolder -ChildPath 'winx64'

    Exec { dotnet publish '.\Leveret\Leveret.csproj' --output $script:publishWinX64Folder --self-contained --runtime win-x64 /p:Version=$version }
}

Task BuildWinX86 -depends PreBuild {
    $script:publishWinX86Folder = Join-Path -Path $script:publishFolder -ChildPath 'winx86'

    Exec { dotnet publish '.\Leveret\Leveret.csproj' --output $script:publishWinX86Folder --self-contained --runtime win-x86 /p:Version=$version }
}

Task BuildLinux64 -depends PreBuild {
    $script:publishLinux64Folder = Join-Path -Path $script:publishFolder -ChildPath 'linux64'

    Exec { dotnet publish '.\Leveret\Leveret.csproj' --output $script:publishLinux64Folder --self-contained --runtime linux-x64 }
}

Task PreBuild -depends Restore {
    $script:publishFolder = Join-Path -Path $script:trashFolder -ChildPath 'bin'
    $script:chocolateyPublishFolderFolder = Join-Path -Path $script:trashFolder -ChildPath 'choco'
    $script:chocoLegal = Join-Path -Path $script:chocolateyPublishFolderFolder -ChildPath 'legal'
    $script:chocoTools = Join-Path -Path $script:chocolateyPublishFolderFolder -ChildPath 'tools'

    New-Item -Path $script:chocoLegal -ItemType Directory | Out-Null
    New-Item -Path $script:chocoTools -ItemType Directory | Out-Null
    New-Item -Path $script:publishFolder -ItemType Directory | Out-Null
}

Task Restore -depends Clean {
    Exec { dotnet restore }
}

Task Clean -depends Init {
    Remove-Item -Path '*/bin' -Recurse -Force
    Remove-Item -Path '*/obj' -Recurse -Force
}

Task Init {
    $date = Get-Date
    $ticks = $date.Ticks
    $trashFolder = Join-Path -Path . -ChildPath '.trash'
    $script:trashFolder = Join-Path -Path $trashFolder -ChildPath $ticks.ToString('D19')
    New-Item -Path $script:trashFolder -ItemType Directory | Out-Null
    $script:trashFolder = Resolve-Path -Path $script:trashFolder
}
