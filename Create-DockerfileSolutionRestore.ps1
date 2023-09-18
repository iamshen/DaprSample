param (
  [string]$solution = "DaprTool.Solution.sln"
)

$outfile = "DockerfileSolutionRestore.txt"

# 该脚本会创建 $outfile 文件，其中包含 Dockerfile 命令，用于还原解决方案的所有软件包，
# 这样你就可以在 "COPY ... "行之前将它们（手工）插入 Dockerfile，
# 从而通过优化 docker build 镜像缓存的使用来提高构建速度。

# 只有在从解决方案中添加或删除项目时才需要该脚本。

Write-Output "" > $outfile 
Add-Content -Path $outfile "# Create this ""restore-solution"" section by running ./Create-DockerfileSolutionRestore.ps1, to optimize build cache reuse"
Select-String -Path $solution -Pattern ', "(.*?\.csproj)"' | ForEach-Object { $_.Matches.Groups[1].Value.Replace("\", "/") } | Sort-Object | ForEach-Object {"COPY [""$_"", """ + $_.Substring(0, $_.LastIndexOf("/") + 1) + """]"} | Out-File -FilePath $outfile -Append
Add-Content -Path $outfile "COPY [""docker-compose.dcproj"", ""./""]"
Add-Content -Path $outfile "COPY [""$solution"", ""./""]"
Add-Content -Path $outfile "RUN dotnet restore ""$solution"""
Add-Content -Path $outfile ""

Get-Content $outfile
