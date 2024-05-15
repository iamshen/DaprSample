
using Aspire.Hosting;
using Aspire.Hosting.Dapr;
using DaprTool.BuildingBlocks.Utils.Constant;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;


var builder = DistributedApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();


// proxy server
builder.AddProject<Projects.ProxyServer>(ApplicationConstants.ProxyServer.AppId)
//.WithReference(webAdmin)
//.WithReference(authAdmin)
//.WithReference(authSts)
//.WithReference(authApi)
//.WithReference(orderApi)
;

// web admin
var webAdmin = builder.AddProject<Projects.WebAdmin>(ApplicationConstants.WebAdmin.AppId)
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = ApplicationConstants.WebAdmin.AppId,
        DaprHttpPort = ApplicationConstants.WebAdmin.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    .WithHttpEndpoint(port: ApplicationConstants.WebAdmin.ResourceHttpPort)
    ;

// auth server
var authSts = builder.AddProject<Projects.Idsrv4_Admin_STS_Identity>(ApplicationConstants.AuthSts.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.AuthSts.AppId,
        DaprHttpPort = ApplicationConstants.AuthSts.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    .WithHttpEndpoint(port: ApplicationConstants.AuthSts.ResourceHttpPort)
    ;

var authAdmin = builder.AddProject<Projects.Idsrv4_Admin>(ApplicationConstants.AuthAdmin.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.AuthAdmin.AppId,
        DaprHttpPort = ApplicationConstants.AuthAdmin.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
    })
    .WithHttpEndpoint(port: ApplicationConstants.AuthAdmin.ResourceHttpPort)
    ;

var authApi = builder.AddProject<Projects.Idsrv4_Admin_Api>(ApplicationConstants.AuthApi.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.AuthApi.AppId,
        DaprHttpPort = ApplicationConstants.AuthApi.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    .WithHttpEndpoint(port: ApplicationConstants.AuthApi.ResourceHttpPort)
    ;


// api services

var orderApi = builder.AddProject<Projects.Ordering_Api>(ApplicationConstants.Ordering.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.Ordering.AppId,
        DaprHttpPort = ApplicationConstants.Ordering.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    .WithHttpEndpoint(port: ApplicationConstants.Ordering.ResourceHttpPort)
    ;


var app = builder.Build();


app.Run();
