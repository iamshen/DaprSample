using Serilog;

Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Debug()
          .WriteTo.Console()
          .WriteTo.File("logs/service-log.txt", rollingInterval: RollingInterval.Hour)
          .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SampleInterfaces.IBankService, SampleService.BankService>();
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<SampleService.SampleActor>();
    options.ReentrancyConfig = new Dapr.Actors.ActorReentrancyConfig()
    {
        Enabled = true,
        MaxStackDepth = 32,
    };
});

builder.Services.AddHttpLogging(options => options.RequestBodyLogLimit = int.MaxValue);

var app = builder.Build();

app.UseHttpLogging();
app.Map("/", () => {

    Log.Debug("Reuqest received Paht: / ");

    return "hello world";
});

app.MapActorsHandlers();


try
{
    Log.Debug("Starting Smaple actor service");
    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    await Log.CloseAndFlushAsync();
}