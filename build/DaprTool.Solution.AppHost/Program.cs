using DaprTool.BuildingBlocks.Utils.Constant;
using Microsoft.Extensions.DependencyInjection;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

var authSts = builder.AddProject<Idsrv4_Admin_STS_Identity>(Constants.AuthSts.AppId)
    .WithDaprSidecar(Constants.AuthSts.GetSideCarOptions())
    .WithExternalHttpEndpoints();

var authAdmin = builder.AddProject<Idsrv4_Admin>(Constants.AuthAdmin.AppId)
    .WithDaprSidecar(Constants.AuthAdmin.GetSideCarOptions())
    .WithExternalHttpEndpoints();

var authApi = builder.AddProject<Idsrv4_Admin_Api>(Constants.AuthApi.AppId)
    .WithDaprSidecar(Constants.AuthApi.GetSideCarOptions())
    .WithExternalHttpEndpoints();

var webAdmin = builder.AddProject<WebAdmin>(Constants.WebAdmin.AppId)
    .WithDaprSidecar(Constants.WebAdmin.GetSideCarOptions())
    .WithReference(authSts)
    .WithReference(authApi)
    .WithExternalHttpEndpoints();

var orderApi = builder.AddProject<Ordering_Api>(Constants.Ordering.AppId)
    .WithDaprSidecar(Constants.Ordering.GetSideCarOptions())
    .WithExternalHttpEndpoints();

builder.AddProject<ProxyServer>(Constants.ProxyServer.AppId)
    .WithReference(webAdmin)
    .WithReference(authAdmin)
    .WithReference(authSts)
    .WithReference(authApi)
    .WithReference(orderApi)
    .WithExternalHttpEndpoints();

var app = builder.Build();

app.Run();