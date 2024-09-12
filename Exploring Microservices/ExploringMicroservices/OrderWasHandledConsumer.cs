using Contracts;
using MassTransit;

namespace ExploringMicroservices
{
    public class OrderWasHandledConsumer : IConsumer<IOrderWasHandled>, IConsumer<IOrderWasNotHandled>
    {
        private readonly ILogger<OrderWasHandledConsumer> _logger;

        public OrderWasHandledConsumer(ILogger<OrderWasHandledConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IOrderWasHandled> context)
        {
            string sHist = context.Message.History + "Вызывающий сервис получил апрув " + DateTime.Now.ToString();
            _logger.LogInformation(sHist);
            _logger.LogInformation("ORDER WAS CONFIRMED!!!!!!!!!!!!!  OK");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<IOrderWasNotHandled> context)
        {
            string sHist = context.Message.History + "Вызывающий сервис вынужден запустить копменсирующую транзакцию " + DateTime.Now.ToString();
            _logger.LogInformation(sHist);
            _logger.LogError("ORDER WAS NOT CONFIRMED!!!!!!!!!!!!!"); // тут в случае отсутствия апрува должен быть запуск компенсирующей транзакции
            return Task.CompletedTask;
        }
    }
}
