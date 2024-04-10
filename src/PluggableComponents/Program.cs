using Dapr.PluggableComponents;
using PluggableComponents;

var app = DaprPluggableComponentsApplication.Create();

app.Logging.AddConsole();

app.RegisterService(
    "npgsqlstore",
    serviceBuilder =>
    {
        // Register one or more components with this service.


        // Use this registration method to have a single state store instance for all components.
        serviceBuilder.RegisterStateStore<PostgreSQLStatestore>();

        // This registration method enables a state store instance per component instance.
        // serviceBuilder.RegisterStateStore(
        //     context =>
        //     {
        //         Console.WriteLine("Creating state store for instance '{0}' on socket '{1}'...", context.InstanceId, context.SocketPath);

        //         return new PostgreSQLStatestore(context.ServiceProvider.GetRequiredService<ILogger<PostgreSQLStatestore>>());
        //     });
    });

System.Console.WriteLine("starting pluggable components...");

app.Run();
