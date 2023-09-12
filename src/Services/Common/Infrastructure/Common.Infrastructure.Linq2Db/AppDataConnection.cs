﻿using Common.Infrastructure.Linq2Db.Entities;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SQLite;

namespace Common.Infrastructure.Linq2Db;

/// <summary>
///     数据库连接管理器
/// </summary>
public class AppDataConnection : DataConnection
{
    public AppDataConnection(DataOptions<AppDataConnection> options) : base(options.Options)
    {
    }

    /// <summary>
    ///     初始化一个 <see cref="AppDataConnection" /> 类型的新实例
    /// </summary>
    /// <param name="connectionString"> 数据库连接字符串 </param>
    public AppDataConnection(string connectionString) : base(SQLiteTools.GetDataProvider(), connectionString)
    {
    }

    public ITable<DistrictTable> Districts => this.GetTable<DistrictTable>();
}