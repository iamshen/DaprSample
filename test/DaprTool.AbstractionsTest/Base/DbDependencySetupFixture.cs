using Microsoft.Extensions.DependencyInjection;

namespace DaprTool.AbstractionsTest.Base;

/// <summary>
///     DataBase Test Fixture
/// </summary>
public class DbDependencySetupFixture : DependencySetupFixture
{
    public override void ConfigurationServices()
    {
        // 注册 业务数据库
        ServiceCollection.AddLogging();
        ServiceCollection.AddAppDataConnection(Configuration);
    }
}

/// <summary>
///     Observer  Test Fixture
/// </summary>
public class ObserverDependencySetupFixture : DependencySetupFixture
{
    public override void ConfigurationServices()
    {
        // 注册 MediatR
        ServiceCollection.AddLogging();
        ServiceCollection.AddAppMediators();
        ServiceCollection.AddAppDataConnection(Configuration);
    }
}

/// <summary>
///     Validation  Test Fixture
/// </summary>
public class ValidationDependencySetupFixture : DependencySetupFixture
{
    public override void ConfigurationServices()
    {
        // 注册 验证器
        ServiceCollection.AddLogging();
        ServiceCollection.AddValidators();
    }
}