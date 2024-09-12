using MassTransit;
using SagasService;

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<OrderConfirmationReqestConsumer>();

            cfg.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
        })
        .AddMassTransitHostedService(true);
    })
    .Build();

await host.RunAsync();