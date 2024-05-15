# 使用 .Net Aspire CLI 将微服务部署到开发环境中 Docker 中

## 准备环境

- 安装 Aspir8 
	- `dotnet tool install -g aspirate --prerelease`
- 配置本地 Docker 注册表：	
	- `docker run -d -p 5001:5000 --restart always --name registry registry:2`

## Quick Start

```
dotnet run --publisher manifest --output-path manifest.json
aspirate init
aspirate build
aspirate generate
aspirate apply
```



## 1. 生成清单文件：导航到“应用主机”项目并执行：

```bash
cd .\build\DaprTool.Solution.AppHost\
dotnet run --publisher manifest --output-path manifest.json
```


## 2. 初始化 Aspir 8

```bash
aspirate init
```


## 3. 使用 Aspir8 通过执行以下命令构建项

```bash
aspirate build
```


## 4. 生成 Kubernetes 文件

```bash
aspirate generate
```


## 5. 运行以下命令进行部署

```bash

aspirate apply
```




# 参考文献

- Aspir8 -  
- https://prom3theu5.github.io/aspirational-manifests/getting-started.html

