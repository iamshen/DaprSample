using DaprTool.AbstractionsTest.Base;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Repository.Entities;

namespace DaprTool.AbstractionsTest;

public class OrderTableCreateTest(DependencySetupFixture fixture) : IClassFixture<DependencySetupFixture>
{
    [Fact(DisplayName = "01. 创建订单表")]
    public async Task Test1()
    {
        using var scope = fixture.ServiceProvider.CreateScope();
        await using var db = scope.ServiceProvider.GetAppConnection();

        await db.DropTableAsync<TradeOrderTable>(throwExceptionIfNotExists: false);
        var table = db.CreateTable<TradeOrderTable>();
        Assert.NotNull(table);

        await db.DropTableAsync<TradeOrderItemTable>(throwExceptionIfNotExists: false);
        var table2 = db.CreateTable<TradeOrderItemTable>();
        Assert.NotNull(table2);
    }
}