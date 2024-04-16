using DaprTool.AbstractionsTest.Base;
using DaprTool.BuildingBlocks.Linq2DbAccessor.Converter;
using DaprTool.BuildingBlocks.Linq2DbAccessor.Data;
using LinqToDB;
using LinqToDB.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace DaprTool.AbstractionsTest.DataBases;

public class JsonQueryTest(DbDependencySetupFixture fixture, ITestOutputHelper testOutputHelper)
    : IClassFixture<DbDependencySetupFixture>
{
    [Fact(DisplayName = "01. JsonWhere")]
    public async Task Test1()
    {
        using var scope = fixture.ServiceProvider.CreateScope();
        await using var db = scope.ServiceProvider.GetAppConnection();

        await db.DropTableAsync<MyJsonTable>(throwExceptionIfNotExists: false);
        var table = db.CreateTable<MyJsonTable>();
        Assert.NotNull(table);

        var affectedRow = await db.InsertAsync(new MyJsonTable
        {
            Id = Guid.NewGuid().ToString(),
            Code = Guid.NewGuid().ToString("N"),
            Remark = "testing",
            CapitalAccount = new CapitalAccount(723453551573140001, "a", Random.Shared.NextDouble()),
            CertificationInfo = new CertificationInfo("张三", "110123...", "18888888881")
        });
        Assert.True(affectedRow > 0);
        var affectedRow1 = await db.InsertAsync(new MyJsonTable
        {
            Id = Guid.NewGuid().ToString(),
            Code = Guid.NewGuid().ToString("N"),
            Remark = "testing1",
            CapitalAccount = new CapitalAccount(723453551573140002, "b", Random.Shared.NextDouble()),
            CertificationInfo = new CertificationInfo("李四", "110456...", "18888888882")
        });
        Assert.True(affectedRow1 > 0);

        var query = db.GetTable<MyJsonTable>()
            .Where(x => x.CapitalAccount != null)
            .JsonWhere(x => x.CapitalAccount.CapitalId == 723453551573140002);
        testOutputHelper.WriteLine($"查询 sql：{query}");

        var row = await query.FirstOrDefaultAsync();
        Assert.NotNull(row);

        testOutputHelper.WriteLine($"查询结果：{row}");
    }

    [Fact(DisplayName = "02. JsonExtractPathText")]
    public async Task Test2()
    {
        using var scope = fixture.ServiceProvider.CreateScope();
        await using var db = scope.ServiceProvider.GetAppConnection();

        await db.DropTableAsync<MyJsonTable>(throwExceptionIfNotExists: false);
        var table = db.CreateTable<MyJsonTable>();
        Assert.NotNull(table);

        var affectedRow = await db.InsertAsync(new MyJsonTable
        {
            Id = Guid.NewGuid().ToString(),
            Code = Guid.NewGuid().ToString("N"),
            Remark = "testing",
            CapitalAccount = new CapitalAccount(723453551573140001, "a", Random.Shared.NextDouble()),
            CertificationInfo = new CertificationInfo("张三", "110123...", "18888888881")
        });
        Assert.True(affectedRow > 0);
        var affectedRow1 = await db.InsertAsync(new MyJsonTable
        {
            Id = Guid.NewGuid().ToString(),
            Code = Guid.NewGuid().ToString("N"),
            Remark = "testing1",
            CapitalAccount = new CapitalAccount(723453551573140002, "b", Random.Shared.NextDouble()),
            CertificationInfo = new CertificationInfo("李四", "110456...", "18888888882")
        });
        Assert.True(affectedRow1 > 0);

        var query = db.GetTable<MyJsonTable>()
            .Where(x => x.CapitalAccount != null)
            .Where(x => x.CertificationInfo.JsonExtractPathText(json => json.RealName) == "张三");
        testOutputHelper.WriteLine($"查询 sql：{query}");

        var row = await query.FirstOrDefaultAsync();
        Assert.NotNull(row);

        testOutputHelper.WriteLine($"查询结果：{row}");
    }
}

[Table(Schema = "public", Name = "t_testing_json_table")]
public class MyJsonTable : EntityBase
{
    [Column("code", CanBeNull = false)] public string Code { get; set; } = string.Empty;

    [Column("remark", CanBeNull = true)] public string? Remark { get; set; } = string.Empty;

    [Column("capital_account", DataType = DataType.BinaryJson, CanBeNull = true)]
    [ValueConverter(ConverterType = typeof(JsonValueConverter<CapitalAccount>))]
    public CapitalAccount? CapitalAccount { get; set; }

    [Column("certification_info", DataType = DataType.BinaryJson, CanBeNull = true)]
    [ValueConverter(ConverterType = typeof(JsonValueConverter<CertificationInfo>))]
    public CertificationInfo? CertificationInfo { get; set; }

    public override string ToString() => Code + Remark;
}

public record CapitalAccount(long CapitalId, string Account, double Balance);

public record CertificationInfo(string RealName, string IdCard, string PhoneNumber);
