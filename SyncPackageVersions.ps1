# 设置相对路径
param(
    [Parameter(Mandatory=$true, HelpMessage="Please provide the path to the source directory. For example: .\src\IdentityServer4")]
    [string]$srcDirectory
)

# 验证目录是否存在
if (-not (Test-Path $srcDirectory)) {
    Write-Error "The specified source directory does not exist: $srcDirectory"
    Write-Host "Please provide a valid directory path. Example: .\src\IdentityServer4"
    return
}

$packagesPropsPath = ".\Directory.Packages.props"

# 读取 Directory.Packages.props 文件以获取现有的包版本
[xml]$packagesProps = Get-Content $packagesPropsPath

# 递归查找所有 csproj 文件
$csprojFiles = Get-ChildItem $srcDirectory -Recurse -Filter "*.csproj"

foreach ($csprojFile in $csprojFiles) {
    [xml]$csproj = Get-Content $csprojFile.FullName

    # 遍历所有 ItemGroup
    foreach ($itemGroup in $csproj.Project.ItemGroup) {
        # 遍历当前 ItemGroup 中的所有 PackageReference 和 DotNetCliToolReference
        foreach ($reference in $itemGroup.PackageReference, $itemGroup.DotNetCliToolReference) {
            $packageName = $reference.Include
            if ([string]::IsNullOrEmpty($packageName)) {
                $packageName = $reference.Update
            }
            $packageVersion = $reference.Version

            # 确保包名称和版本都不为空
            if ([string]::IsNullOrWhiteSpace($packageName) -or [string]::IsNullOrWhiteSpace($packageVersion)) {
                Write-Host "Skipping invalid reference in $($csprojFile.Name)"
                continue
            }

            # 检查 PackageVersion 是否已在 Directory.Packages.props 中定义
            $existingPackage = $packagesProps.Project.ItemGroup.PackageVersion | Where-Object { $_.Include -eq $packageName }

            if (-not $existingPackage) {
                # 如果不存在，则添加新的 PackageVersion
                $newNode = $packagesProps.CreateElement("PackageVersion")
                $newNode.SetAttribute("Include", $packageName)
                $newNode.SetAttribute("Version", $packageVersion)
                $packagesProps.Project.ItemGroup.AppendChild($newNode) | Out-Null
            }

            # 检查是否存在 Version 属性，并移除它
            if ($reference.Version) {
                $reference.RemoveAttribute("Version")
            }
        }
    }

    # 保存修改后的 csproj 文件
    $csproj.Save($csprojFile.FullName)
}

# 保存修改后的 Directory.Packages.props 文件
$packagesProps.Save($packagesPropsPath)

