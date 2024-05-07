﻿global using System;
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Idsrv4.Admin.EntityFramework.Shared.DbContexts;
global using Idsrv4.Admin.EntityFramework.Shared.Entities.Identity;
global using Idsrv4.Admin.Shared.Configuration.Helpers;
global using Idsrv4.Admin.STS.Identity.Configuration;
global using Idsrv4.Admin.STS.Identity.Configuration.Constants;
global using Idsrv4.Admin.STS.Identity.Configuration.Interfaces;
global using Idsrv4.Admin.STS.Identity.Helpers;
global using System.IO;
global using Serilog;