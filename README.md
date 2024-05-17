# 使用 Dapr 构建分布式应用测试项目


# 先决条件

### 基础设施

- **[`Dapr`](https://dapr.io/)**
- **[`Dotnet`](https://dotnet.microsoft.com/download)** 
- **[`WSL2 - Ubuntu OS`](https://docs.microsoft.com/en-us/windows/wsl/install-win10)**
- **[`.NET 8.0`](https://dotnet.microsoft.com/download)** 

### 构建编排

- **[`.NET Aspire`](https://github.com/dotnet/aspire)** 
- **[`Aspir8`](https://prom3theu5.github.io/aspirational-manifests/getting-started.html)**
- **[`Docker for desktop`](https://www.docker.com/products/docker-desktop)** 

### 后端服务

- **[`Yarp`](https://github.com/microsoft/reverse-proxy)**
- **[`MediatR`](https://github.com/jbogard/MediatR)**
- **[`Linq2Db`](https://github.com/linq2db/linq2db)**
- **[`Serilog`](https://github.com/serilog/serilog)**
- **[`IdentityServer4`](https://github.com/iamshen/Reborn.IdentityServer4.Admin)**
- **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)**
- **[`Swashbuckle.AspNetCore`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** 

### 前端 UI

- **[`ASP.NET Core Web UI(Blazor)`](https://learn.microsoft.com/zh-cn/aspnet/core/tutorials/choose-web-ui?view=aspnetcore-8.0)**
- **[`Fluent UI Blazor`](https://www.fluentui-blazor.net/)**

### 编程规范

- **[`适用于 .NET/.NET Core 的代码整洁之道`](https://github.com/thangchung/clean-code-dotnet/blob/master/README-zh.md)**

# Dapr 配置

[详情查看](./CONFIGURATION.md)

# 可插拔组件

> 本地测试 可插拔组件的时候  请使用 wsl 运行 dotnet run



### IdentityServer4 

#### 数据库准备

* 1. 创建数据库
  - `1.1 创建 postgres 用户 idsrv4 密码 Local@Db` 
  - `1.2 创建 postgres 数据库 CREATE DATABASE idsrv4 OWNER idsrv4;`
* 2. 初始化数据库
  - `2.1 cd dapr-tool-solution\src\IdentityServer4\src\Idsrv4.Admin`
  - `2.2 dotnet run /seed`