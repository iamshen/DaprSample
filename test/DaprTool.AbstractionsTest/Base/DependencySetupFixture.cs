using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Google.Rpc.Help.Types;
using Xunit.Sdk;
using Xunit.Abstractions;

namespace DaprTool.AbstractionsTest.Base;

/// <summary>
///     Test Fixture
/// </summary>
public class DependencySetupFixture : IDisposable
{

    /// <summary>
    ///     ctor
    /// </summary>
    public DependencySetupFixture(IMessageSink sink)
    {
        this.Sink = sink;
        var serviceCollection = new ServiceCollection();

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
        configurationBuilder.AddJsonFile("appsettings.json", true, true);

        var configuration = configurationBuilder.Build();

        // 注册 MediatR
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencySetupFixture).Assembly));
        // 注册 业务数据库
        serviceCollection.AddAppDataConnection(configuration.GetConnectionString("OrderingDB")!);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    /// <summary>
    ///     ServiceProvider
    /// </summary>
    public IServiceProvider ServiceProvider { get; private set; }
    public IMessageSink Sink;

    public void Dispose()
    {
        Sink.OnMessage(new DiagnosticMessage("DIAG TEST DISPOSE"));
    }
}