# 使用 Dapr 构建分布式应用测试项目

# QuickStart 


```bash

git clone git@gitee.com:gold-cloud/dapr-tool-solution.git

visual studio 2022 p
```

# 先决条件

### 基础设施

- **[`Visual Studio 2022 预览版`](https://visualstudio.microsoft.com/zh-hans/vs/preview/)**
- **[`.NET 8.0 +`](https://dotnet.microsoft.com/download)**  -安装了 vs 后会自带 无需单独安装
- **[`Dapr`](https://dapr.io/)**
- **[`WSL2 - Ubuntu OS`](https://docs.microsoft.com/en-us/windows/wsl/install-win10)**
- **[`Aspir8`](https://prom3theu5.github.io/aspirational-manifests/getting-started.html)**
- **[`Docker for desktop`](https://www.docker.com/products/docker-desktop)**
- **[`RabbitMQ`](https://gitee.com/iamshen/my-docker-compose/blob/master/rabbitmq/README.MD)** 

### 后端

- **[`.NET Aspire`](https://github.com/dotnet/aspire)** 
- **[`Yarp`](https://github.com/microsoft/reverse-proxy)**
- **[`MediatR`](https://github.com/jbogard/MediatR)**
- **[`Linq2Db`](https://github.com/linq2db/linq2db)**
- **[`Serilog`](https://github.com/serilog/serilog)**
- **[`IdentityServer4`](https://github.com/iamshen/Reborn.IdentityServer4.Admin)**
- **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)**
- **[`Swashbuckle.AspNetCore`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** 

### 前端

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
  - `2.1 把 Idsrv4.Admin | Idsrv4.Admin.Api | Idsrv4.Admin.STS.Identity 这三个项目中 appsettings.json 的所有 ConnectionStrings 的连接字符串修改为自己本地 localhost`
  - `2.2 cd dapr-tool-solution\src\IdentityServer4\src\Idsrv4.Admin`
  - `2.3 dotnet run /seed`