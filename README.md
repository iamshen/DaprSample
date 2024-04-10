# 使用 Dapr 构建分布式应用测试项目


# QuickStart

> 请先[配置开发环境](#基础设施)

```bash

# 1.克隆代码

git clone git@gitee.com:gold-cloud/dapr-tool-solution.git

# 2. 运行

cd dapr-tool-solution

dapr run -f .\src\Services\Ordering

# 
# dapr run -f YourDaprYamlPath

```


# 先决条件

### 基础设施

- **[`Docker for desktop`](https://www.docker.com/products/docker-desktop)** 
- **[`Dapr`](https://dapr.io/)**
- **[`Dotnet`](https://dotnet.microsoft.com/download)** 
- **[`WSL2 - Ubuntu OS`](https://docs.microsoft.com/en-us/windows/wsl/install-win10)**

### 后端

- **[`.NET Core 8 +`](https://dotnet.microsoft.com/download)** 
- **[`.NET Aspire`](https://github.com/dotnet/aspire)** 
- **[`NEST`](https://github.com/elastic/elasticsearch-net)**
- **[`Yarp`](https://github.com/microsoft/reverse-proxy)**
- **[`MediatR`](https://github.com/jbogard/MediatR)**
- **[`Linq2Db`](https://github.com/linq2db/linq2db)**
- **[`Serilog`](https://github.com/serilog/serilog)**
- **[`IdentityServer4`](https://identityserver.io)**
- **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)**
- **[`Swashbuckle.AspNetCore`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** 

### 前端

- **[`ASP.NET Core Web UI`](https://learn.microsoft.com/zh-cn/aspnet/core/tutorials/choose-web-ui?view=aspnetcore-8.0)**
- **[`Nodejs 20.x`](https://nodejs.org/en/download)**
- **[`Typescript`](https://www.typescriptlang.org)**
- **[`Vite`](https://cn.vitejs.dev/guide/)**




# Dpar 配置


[详情查看](./CONFIGURATION.md)


# 可插拔组件

> 本地测试 可插拔组件的时候  请使用 wsl 运行 dotnet run
