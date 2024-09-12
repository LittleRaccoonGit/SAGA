using ExploringMicroservices;
using ExploringMicroservices.Repositories;
using MassTransit;
using StateMachine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<OrderWasHandledConsumer>();
    //cfg.AddSagaStateMachine<OrderSateMachine, OrderState>()
    //  .InMemoryRepository();
    cfg.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
})
    .AddMassTransitHostedService(true);

builder.Services.AddMvc();

builder.Services.AddSingleton<IOrderExampleRepository, OrderExampleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();