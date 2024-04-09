using DaprTool.AbstractionsTest.Base;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Repository.Entities;

namespace DaprTool.AbstractionsTest;

public class OrderTableCreateTest(DependencySetupFixture fixture) : IClassFixture<DependencySetupFixture>
{
    [Fact(DisplayName = "01. 创建买料订单数据表")]
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
}