
using Aspire.Hosting.Dapr;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;

var builder = DistributedApplication.CreateBuilder(args);


builder.Services.AddProblemDetails();

// proxy server
builder.AddProject<Projects.ProxyServer>(AppConstants.ProxyServerApp)
    ;

// web admin
builder.AddProject<Projects.WebAdmin>(AppConstants.WebAdminApp)
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = AppConstants.WebAdminApp,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(AppConstants.ResourcesPath),
    })
    .WithHttpEndpoint(port: 51871)
    .WithHttpsEndpoint(port: 51872, name: "webAdminHttpsEndPoint")
    ;

// auth server
builder.AddProject<Projects.Idsrv4_Admin_STS_Identity>(AppConstants.AuthStsApp)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = AppConstants.AuthStsApp,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(AppConstants.ResourcesPath),
    })
    .WithHttpEndpoint(port: 52871)
    .WithHttpsEndpoint(port: 52873, name: "authStsHttpsEndPoint");

builder.AddProject<Projects.Idsrv4_Admin>(AppConstants.AuthAdminApp)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = AppConstants.AuthAdminApp,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(AppConstants.ResourcesPath),
    })
    .WithHttpEndpoint(port: 53871)
    .WithHttpsEndpoint(port: 53873, name: "authAdminHttpsEndPoint");

builder.AddProject<Projects.Idsrv4_Admin_Api>(AppConstants.AuthApiApp)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = AppConstants.AuthApiApp,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(AppConstants.ResourcesPath),
    })
    .WithHttpEndpoint(port: 54871)
    .WithHttpsEndpoint(port: 54873, name: "authApiHttpsEndPoint")
    ;


// api services

builder.AddProject<Projects.Ordering_Api>(AppConstants.OrderApiApp)
    .WithDaprSidecar(new DaprSidecarOptions()
    {
        AppId = AppConstants.OrderApiApp,
        ResourcesPaths = ImmutableHashSet<string>.Empty.Add(AppConstants.ResourcesPath),
    })
    .WithHttpEndpoint(port: 31441)
    .WithHttpsEndpoint(port: 31443, name: "orderApiHttpsEndPoint")
    ;


var app = builder.Build();


app.Run();
