using SampleInterfaces;
using SampleService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IBankService, BankService>();


builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<SampleActor>();
});

var app = builder.Build();

app.MapActorsHandlers();


Console.WriteLine("starting sapmle service...");
app.Run();
