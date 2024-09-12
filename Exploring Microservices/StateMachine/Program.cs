using MassTransit;
using Microsoft.Extensions.Hosting;
using StateMachine;

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
    services.AddMassTransit(cfg =>
    {
        cfg.AddSagaStateMachine<OrderSateMachine, OrderState>()
        .InMemoryRepository();

        cfg.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
    })
    .AddMassTransitHostedService(true);
    })
    .Build();

await host.RunAsync();