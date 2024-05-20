using DaprTool.BuildingBlocks.Utils.Constant;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

// web admin
var webAdmin = builder.AddProject<Projects.WebAdmin>(Constants.WebAdmin.AppId)
    .WithDaprSidecar(Constants.WebAdmin.GetSideCarOptions())
    .WithHttpEndpoint(port: Constants.WebAdmin.ResourceHttpPort);

// auth-server
var authSts = builder.AddProject<Projects.Idsrv4_Admin_STS_Identity>(Constants.AuthSts.AppId)
    .WithDaprSidecar(Constants.AuthSts.GetSideCarOptions())
    .WithHttpEndpoint(port: Constants.AuthSts.ResourceHttpPort);

var authAdmin = builder.AddProject<Projects.Idsrv4_Admin>(Constants.AuthAdmin.AppId)
    .WithDaprSidecar(Constants.AuthAdmin.GetSideCarOptions())
    .WithHttpEndpoint(port: Constants.AuthAdmin.ResourceHttpPort);

var authApi = builder.AddProject<Projects.Idsrv4_Admin_Api>(Constants.AuthApi.AppId)
    .WithDaprSidecar(Constants.AuthApi.GetSideCarOptions())
    .WithHttpEndpoint(port: Constants.AuthApi.ResourceHttpPort);

// api services
var orderApi = builder.AddProject<Projects.Ordering_Api>(Constants.Ordering.AppId)
    .WithDaprSidecar(Constants.Ordering.GetSideCarOptions())
    .WithHttpEndpoint(port: Constants.Ordering.ResourceHttpPort);

// proxy server
builder.AddProject<Projects.ProxyServer>(Constants.ProxyServer.AppId)
      .WithReference(webAdmin)
      .WithReference(authAdmin)
      .WithReference(authSts)
      .WithReference(authApi)
      .WithReference(orderApi);

var app = builder.Build();

app.Run();
