Properties {
    $version="0.0.1"
}

Task PublishChocolateyPackage -Depends PackChocolateyPackage

Task PackChocolateyPackage -Depends ZipBuildArtifacts {
    Copy-Item -Path .\Chocolatey\tools\chocolateyInstall.ps1 -Destination chocoTools
    Exec { choco pack ".\Chocolatey\leveret.nuspec" --version $version --outputdirectory $script:chocolateyOutputWin7x64Folder version=$version }
}

Task ZipBuildArtifacts -Depends BuildWin7x64,BuildWin7x86 {
    $script:artifactsZipX64 = Join-Path -Path $script:chocoTools -ChildPath "win7x64.zip"
    $script:artifactsZipX86 = Join-Path -Path $script:chocoTools -ChildPath "win7x86.zip"

    Compress-Archive -Path "$script:publishWin7x64Folder\*" -CompressionLevel Optimal -DestinationPath $script:artifactsZipX64
    Compress-Archive -Path "$script:publishWin7x86Folder\*" -CompressionLevel Optimal -DestinationPath $script:artifactsZipX86
}

Task Build -Depends BuildWin7x64,BuildWin7x86,BuildLinux64,BuildRhel64

Task BuildWin7x64 -Depends PreBuild {
   $script:publishWin7x64Folder = Join-Path -Path $script:publishFolder -ChildPath "win7x64"

   Exec { dotnet publish ".\Leveret\Leveret.csproj" --output $script:publishWin7x64Folder --self-contained --runtime win7-x64 }
}

Task BuildWin7x86 -Depends PreBuild {
    $script:publishWin7x86Folder = Join-Path -Path $script:publishFolder -ChildPath "win7x86"
 
    Exec { dotnet publish ".\Leveret\Leveret.csproj" --output $script:publishWin7x86Folder --self-contained --runtime win7-x86 }
}

Task BuildLinux64 -Depends PreBuild {
    $script:publishLinux64Folder = Join-Path -Path $script:publishFolder -ChildPath "linux64"
 
    Exec { dotnet publish ".\Leveret\Leveret.csproj" --output $script:publishLinux64Folder --self-contained --runtime linux-x64 }
}

Task BuildRhel64 -Depends PreBuild {
    $script:publishRhel64Folder = Join-Path -Path $script:publishFolder -ChildPath "rhel64"
 
    Exec { dotnet publish ".\Leveret\Leveret.csproj" --output $script:publishRhel64Folder --self-contained --runtime rhel-x64 }
}

Task PreBuild -Depends Init,Clean {
    $script:publishFolder = Join-Path -Path $script:trashFolder -ChildPath "bin"
    $script:chocolateyPublishFolderFolder = Join-Path -Path $script:trashFolder -ChildPath "choco"
    $script:chocoTools = Join-Path -Path $script:chocolateyPublishFolderFolder -ChildPath "tools"
    
    New-Item -Path $script:chocoTools -ItemType Directory | Out-Null
    New-Item -Path $script:publishFolder -ItemType Directory | Out-Null
}
Task Clean -Depends Init {
    Remove-Item -Path "*/bin" -Recurse -Force
    Remove-Item -Path "*/obj" -Recurse -Force
}

Task Init {
   $date = Get-Date
   $ticks = $date.Ticks
   $trashFolder = Join-Path -Path . -ChildPath ".trash"
   $script:trashFolder = Join-Path -Path $trashFolder -ChildPath $ticks.ToString("D19")
   New-Item -Path $script:trashFolder -ItemType Directory | Out-Null
   $script:trashFolder = Resolve-Path -Path $script:trashFolder
}
 