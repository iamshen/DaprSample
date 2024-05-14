
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
    ;


//var customContainer = builder.AddContainer("web-admin-container", "web-admin-img")
//                           .WithHttpEndpoint(port: 9043, targetPort: 9044, name: "endpoint");

//var endpoint = customContainer.GetEndpoint("endpoint");

// web admin
builder.AddProject<Projects.WebAdmin>(ApplicationConstants.WebAdmin.AppId)
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = ApplicationConstants.WebAdmin.AppId,
        DaprHttpPort = ApplicationConstants.WebAdmin.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    //.WithReference(endpoint)
    .WithHttpEndpoint(port: ApplicationConstants.WebAdmin.ResourceHttpPort)
    //.WithHttpsEndpoint(port: ApplicationConstants.WebAdmin.ResourceHttpsPort, name: ApplicationConstants.WebAdmin.ResourceHttpsEndpoint)
    ;

// auth server
builder.AddProject<Projects.Idsrv4_Admin_STS_Identity>(ApplicationConstants.AuthSts.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.AuthSts.AppId,
        DaprHttpPort = ApplicationConstants.AuthSts.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    .WithHttpEndpoint(port: ApplicationConstants.AuthSts.ResourceHttpPort)
    .WithHttpsEndpoint(port: ApplicationConstants.AuthSts.ResourceHttpsPort, name: ApplicationConstants.AuthSts.ResourceHttpsEndpoint);

builder.AddProject<Projects.Idsrv4_Admin>(ApplicationConstants.AuthAdmin.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.AuthAdmin.AppId,
        DaprHttpPort = ApplicationConstants.AuthAdmin.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
    })
    .WithHttpEndpoint(port: ApplicationConstants.AuthAdmin.ResourceHttpPort)
    .WithHttpsEndpoint(port: ApplicationConstants.AuthAdmin.ResourceHttpsPort, name: ApplicationConstants.AuthAdmin.ResourceHttpsEndpoint);

builder.AddProject<Projects.Idsrv4_Admin_Api>(ApplicationConstants.AuthApi.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.AuthApi.AppId,
        DaprHttpPort = ApplicationConstants.AuthApi.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    .WithHttpEndpoint(port: ApplicationConstants.AuthApi.ResourceHttpPort)
    .WithHttpsEndpoint(port: ApplicationConstants.AuthApi.ResourceHttpsPort, name: ApplicationConstants.AuthApi.ResourceHttpsEndpoint)
    ;


// api services

builder.AddProject<Projects.Ordering_Api>(ApplicationConstants.Ordering.AppId)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = ApplicationConstants.Ordering.AppId,
        DaprHttpPort = ApplicationConstants.Ordering.DaprHttpPort,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(ApplicationConstants.ResourcesPath),
        DaprHttpMaxRequestSize = 60,
        DaprHttpReadBufferSize = 128,
    })
    .WithHttpEndpoint(port: ApplicationConstants.Ordering.ResourceHttpPort)
    .WithHttpsEndpoint(port: ApplicationConstants.Ordering.ResourceHttpsPort, name: ApplicationConstants.Ordering.ResourceHttpsEndpoint)
    ;


var app = builder.Build();


app.Run();
