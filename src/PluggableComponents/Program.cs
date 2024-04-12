using Dapr.PluggableComponents;
using PluggableComponents;

var app = DaprPluggableComponentsApplication.Create();

app.Logging.AddConsole();

app.RegisterService(
    "hstore",
    serviceBuilder =>
    {
        // Register one or more components with this service.


        // Use this registration method to have a single state store instance for all components.
        // serviceBuilder.RegisterStateStore<HStatestore>();

        // This registration method enables a state store instance per component instance.
        serviceBuilder.RegisterStateStore(
            context =>
            {
                Console.WriteLine("Creating state store for instance '{0}' on socket '{1}'...", context.InstanceId, context.SocketPath);

                return new HStatestore(context.ServiceProvider.GetRequiredService<ILogger<HStatestore>>());
            });
    });

System.Console.WriteLine("starting pluggable components...");

app.Run();
