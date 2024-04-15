using DaprTool.AbstractionsTest.Base;
using DaprTool.BuildingBlocks.Linq2DbAccessor.Data;
using LinqToDB;
using LinqToDB.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Repository.Entities;

namespace DaprTool.AbstractionsTest.Validation;

public class OrderTableTest(DbDependencySetupFixture fixture) : IClassFixture<DbDependencySetupFixture>
{
    [Fact(DisplayName = "01. CreateTable")]
    public async Task Test1()
    {
        using var scope = fixture.ServiceProvider.CreateScope();
        await using var db = scope.ServiceProvider.GetAppConnection();

        await db.DropTableAsync<PurchaseOrderTable>(throwExceptionIfNotExists: false);
        var table = db.CreateTable<PurchaseOrderTable>();
        Assert.NotNull(table);

        await db.DropTableAsync<PurchaseOrderItemTable>(throwExceptionIfNotExists: false);
        var table2 = db.CreateTable<PurchaseOrderItemTable>();
        Assert.NotNull(table2);
    }

    [Fact(DisplayName = "02. Complex")]
    public async Task Test2()
    {
        using var scope = fixture.ServiceProvider.CreateScope();
        await using var db = scope.ServiceProvider.GetAppConnection();

        await db.DropTableAsync<TestingOrderTable>(throwExceptionIfNotExists: false);

        // Table
        var table = db.CreateTable<TestingOrderTable>();
        Assert.NotNull(table);

        // Insert
        var affectedRow = await db.InsertAsync(new TestingOrderTable
        {
            Id = Guid.NewGuid().ToString("N"),
            Code = Guid.NewGuid()
        });
        Assert.True(affectedRow > 0);

        // Query
        var row = await db.GetTable<TestingOrderTable>().FirstOrDefaultAsync();
        Assert.NotNull(row);
    }
}

[Table(Schema = "public", Name = "t_testing_order")]
public class TestingOrderTable : EntityBase
{
    /// <summary>
    ///     Code
    /// </summary>
    [Column("code", CanBeNull = false)]
    public Guid Code { get; set; }
}
