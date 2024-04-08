# 使用 Dapr 构建分布式应用测试项目



## 1. 先决条件

### 基础设施

- **[`WSL2 - Ubuntu OS`](https://docs.microsoft.com/en-us/windows/wsl/install-win10)**
- **[`Docker for desktop`](https://www.docker.com/products/docker-desktop)** 
- **[`Dapr`](https://dapr.io/)**
- **[`.NET Aspire`](https://github.com/dotnet/aspire)** 

### 后端

- **[`.NET Core 8 +`](https://dotnet.microsoft.com/download)** 
- **[`IdentityServer4`](https://identityserver.io)**
- **[`Yarp`](https://github.com/microsoft/reverse-proxy)**
- **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)**
- **[`MediatR`](https://github.com/jbogard/MediatR)**
- **[`Linq2Db`](https://github.com/linq2db/linq2db)**
- **[`Scrutor`](https://github.com/khellang/Scrutor)** - 通过扫描类型所在的程序集并找到约定的类型提供自动注册服务。（使用的是默认的依赖注入容器）
- **[`Serilog`](https://github.com/serilog/serilog)**
- **[`Swashbuckle.AspNetCore`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** 
- **[`NEST`](https://github.com/elastic/elasticsearch-net)**

### 前端

- **[`ASP.NET Core Web UI`](https://learn.microsoft.com/zh-cn/aspnet/core/tutorials/choose-web-ui?view=aspnetcore-8.0)**
- **[`Nodejs 20.x`](https://nodejs.org/en/download)**
- **[`Typescript`](https://www.typescriptlang.org)**
- **[`Vite`](https://cn.vitejs.dev/guide/#scaffolding-your-first-vite-project)**


## 2. Dpar 配置

[详情查看](./CONFIGURATION.md)


## 3. QuickStart


```bash

git clone git@gitee.com:gold-cloud/dapr-tool-solution.git

cd dapr-tool-solution

dapr run -f .\src\Services\Ordering

```