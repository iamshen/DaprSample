using System;
using System.Reflection;
using Idsrv4.Admin.EntityFramework.Configuration.Configuration;
using PostgreSQLMigrationAssembly = Idsrv4.Admin.EntityFramework.PostgreSQL.Helpers.MigrationAssembly;

namespace Idsrv4.Admin.Configuration.Database;

public static class MigrationAssemblyConfiguration
{
    public static string GetMigrationAssemblyByProvider(DatabaseProviderConfiguration databaseProvider)
    {
        return databaseProvider.ProviderType switch
        {
            DatabaseProviderType.PostgreSQL => typeof(PostgreSQLMigrationAssembly).GetTypeInfo()
                .Assembly.GetName()
                .Name,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}