﻿global using System;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Serilog;
global using System.Linq;
global using System.Threading.Tasks;
global using Idsrv4.Admin.EntityFramework.Configuration.Configuration;
global using Idsrv4.Admin.EntityFramework.Shared.Helpers;
global using Idsrv4.Admin.AuditLogging.EntityFramework.Entities;
global using Idsrv4.Admin.Configuration.Database;
global using Idsrv4.Admin.EntityFramework.Shared.DbContexts;
global using Idsrv4.Admin.EntityFramework.Shared.Entities.Identity;
global using Idsrv4.Admin.Helpers;
global using Idsrv4.Admin.Shared.Configuration.Helpers;
global using Idsrv4.Admin.Shared.Dtos;
global using Idsrv4.Admin.Shared.Dtos.Identity;