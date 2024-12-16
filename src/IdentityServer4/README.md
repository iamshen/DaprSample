# Idsrv4.Admin


![CI](https://github.com/iamshen/Idsrv4.Admin/workflows/CI/badge.svg)
[![NuGet](https://img.shields.io/nuget/dt/Idsrv4.Admin.Templates.svg)](https://www.nuget.org/packages/Idsrv4.Admin.Templates) 
[![NuGet](https://img.shields.io/nuget/vpre/Idsrv4.Admin.Templates.svg)](https://www.nuget.org/packages/Idsrv4.Admin.Templates)


The administration for the IdentityServer4 and Asp.Net Core Identity <br>
The application is written in the **Asp.Net Core MVC - using .NET 8**


## QuickStart 

### Requirements

- [Install](https://www.microsoft.com/net/download/windows#/current) the latest .NET 8 SDK 


### Installation via dotnet new template

```bash
# 安装/更新
dotnet new install Idsrv4.Admin.Templates
# 卸载
dotnet new uninstall Idsrv4.Admin.Templates
```

### Create new project:

new solution

```bash
dotnet new reborn.is4admin --name SampleIds4.Admin --title "Sample IdentityServer4 Admin" --adminrole Administrator --adminclientid sample_identity_admin --adminclientsecret sample_admin_client_secret --force

```

options:

```bash
--name: [string value] The project name

--title: [string value] The title and footer of the administration

--adminrole: [string value] The name of admin role, that is used to authorize the 

--adminclientid: [string value] The name of client, that is be used in the IdentityServer4

--adminclientsecret: [string value] The value of client secret, that is be used in the IdentityServer4
```




# Note

> This project, modified by iamshen from [IdentityServer4.Admin](https://github.com/skoruba/IdentityServer4.Admin), has been upgraded to .NET 8.

> The project also references [Reborn.IdentityServer4](https://www.nuget.org/packages/Reborn.IdentityServer4), which also supports .NET 8.



# Migrations 

1. 设置启动项目为 Idsrv4.Admin
2. 打开 程序包管理器控制台 设置默认项目为 Idsrv4.Admin.EntityFramework.PostgreSQL
3. 新增活修改实体字段后 使用对应的 数据库上下文生成新的迁移文件
4. 运行 Idsrv4.Admin 项目 应用迁移 `dotnet run /migrateonly`

```

Add-Migration DbInit -Context AdminAuditLogDbContext -OutputDir ../Idsrv4.Admin.EntityFramework.PostgreSQL/Migrations/AuditLogging

Add-Migration DbInit -Context IdentityServerDataProtectionDbContext -OutputDir ../Idsrv4.Admin.EntityFramework.PostgreSQL/Migrations/DataProtection

Add-Migration DbInit -Context AdminIdentityDbContext -OutputDir ../Idsrv4.Admin.EntityFramework.PostgreSQL/Migrations/Identity

Add-Migration DbInit -Context IdentityServerConfigurationDbContext -OutputDir ../Idsrv4.Admin.EntityFramework.PostgreSQL/Migrations/IdentityServerConfiguration

Add-Migration DbInit -Context IdentityServerPersistedGrantDbContext -OutputDir ../Idsrv4.Admin.EntityFramework.PostgreSQL/Migrations/IdentityServerGrants

Add-Migration DbInit -Context AdminLogDbContext -OutputDir ../Idsrv4.Admin.EntityFramework.PostgreSQL/Migrations/Logging

```
