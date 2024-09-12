using MassTransit;
using Contracts;
using System;

namespace SagasService
{
    public class OrderConfirmationReqestConsumer : IConsumer<IConfirmationReqested>
    {
        private readonly ILogger<OrderConfirmationReqestConsumer> _logger;

        public OrderConfirmationReqestConsumer(ILogger<OrderConfirmationReqestConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IConfirmationReqested> context)
        {
            //Засыпаем на 5 секунд, ну как бы думаем ))))
            _logger.LogInformation("---------------Sleep " + DateTime.Now.ToString());
            System.Threading.Thread.Sleep(5000); //Пауза 5 секунд
            _logger.LogInformation("--------------EndSleep " + DateTime.Now.ToString());

            //генерим случайное число от 0 до 10
            var rand = new Random();
            int rvalue = rand.Next(0, 10);

            //если случйное число больше 5 то реджект, иначе конфирм
            if (rvalue > 5)
            {
                await context.Publish<IOrderRejected>(new
                {
                    OrderId = context.Message.OrderId,
                    History = context.Message.History + "Отказано в апруве " + DateTime.Now.ToString() + (char)13 + (char)10
                });
            }
            else
            {
                await context.Publish<IOrderConfirmed>(new
                {
                    OrderId = context.Message.OrderId,
                    History = context.Message.History + "Получен апрув " + DateTime.Now.ToString() + (char)13 + (char)10
                });
            }
        }
    }
}
