using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DaprTool.AbstractionsTest.Base;

public abstract class DependencySetupFixture
{
    protected DependencySetupFixture()
    {
        ServiceCollection = new ServiceCollection();

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
        configurationBuilder.AddJsonFile("appsettings.json", true, true);

        Configuration = configurationBuilder.Build();

        Init();
    }

    public void Init()
    {
        ConfigurationServices();
    }

    /// <summary>  ServiceProvider </summary>
    public IServiceProvider ServiceProvider => ServiceCollection.BuildServiceProvider();

    /// <summary> IServiceCollection </summary>
    public IServiceCollection ServiceCollection { get; set; }

    public IConfigurationRoot Configuration { get; set; }

    public abstract void ConfigurationServices();
}