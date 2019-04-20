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
    $script:publishFolder = Join-Path -Path $script:trashFolder -ChildPath "publish"

    New-Item -Path $script:publishFolder -ItemType Directory
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
   New-Item -Path $script:trashFolder -ItemType Directory
   $script:trashFolder = Resolve-Path -Path $script:trashFolder
}
 