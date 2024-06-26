﻿using DaprTool.BuildingBlocks.Utils.Constant;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.Extensions.Configuration;
using Ordering.Infrastructure.Repository;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     AppDataConnection Extensions
/// </summary>
public static class AppDataConnectionExtensions
{
    /// <summary>
    ///     获取 DataConnection
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static AppDataConnection GetAppConnection(this IServiceProvider service)
    {
        var connection = service.GetService<AppDataConnection>();

        if (connection is null)
            throw new ArgumentException(nameof(connection));

        return connection;
    }

    /// <summary>
    ///     注册 DataConnection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static void AddOrderAppDataConnection(this IServiceCollection services, string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException("connectionString can not be null", nameof(connectionString));

        services.AddLinqToDBContext<AppDataConnection>((provider, options)
                => options
                    .UsePostgreSQL(connectionString)
                    .UseDefaultLogging(provider),
            ServiceLifetime.Transient);
    }

    /// <summary>
    ///     注册 DataConnection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static void AddOrderAppDataConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOrderAppDataConnection(configuration.GetConnectionString(Constants.Ordering.AppId)!);
    }
}