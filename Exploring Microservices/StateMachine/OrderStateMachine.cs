using System;
using System.Collections.Generic;
using Automatonymous;
using Contracts;
using MassTransit;

namespace StateMachine
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
        public string Name { get; set; }
        public string History { get; set; }
    }

    public class OrderSateMachine : MassTransitStateMachine<OrderState>
    { 
        public OrderSateMachine() 
        {
            InstanceState(x => x.CurrentState);
            Event(() => OrderSubmitted, context => context.CorrelateById(x => x.Message.OrderId));
            Event(() => OrderConfirmed, context => context.CorrelateById(x => x.Message.OrderId));
            Event(() => OrderRejected, context => context.CorrelateById(x => x.Message.OrderId));

            Initially(When(OrderSubmitted)
                .Then(context =>
                {
                    context.Instance.CorrelationId = context.Data.OrderId;
                    context.Instance.Name = context.Data.Name;
                    context.Instance.History = context.Data.History; 
                })
                .PublishAsync(context => context.Init<IConfirmationReqested>(new
                {
                    OrderId = context.Instance.CorrelationId,
                    Name = context.Instance.Name,
                    History = context.Data.History + "Сохранили состояние SAGA в StateMachine и запросили апрув " + DateTime.Now.ToString() + (char)13 + (char)10
        }
                ))
                .TransitionTo(AwaitingConfirmation));

            During(AwaitingConfirmation,
                When(OrderConfirmed)
                .PublishAsync(x => x.Init<IOrderWasHandled>(new 
                {
                    OrderId = x.Instance.CorrelationId,
                    History = x.Data.History + "StateMachine получила апрув и сообщила об этом вызывающему сервису " + DateTime.Now.ToString() + (char)13 + (char)10
                }))
                .Finalize(),
                When(OrderRejected)
                .PublishAsync(x => x.Init<IOrderWasNotHandled>(new {
                    OrderId = x.Instance.CorrelationId,
                    History = x.Data.History + "StateMachine получила reject и сообщила об этом вызывающему сервису " + DateTime.Now.ToString() + (char)13 + (char)10
                }))
                .Finalize());
        }
        public Event<IOrderSubmited> OrderSubmitted { get; set; }
        public Event<IOrderConfirmed> OrderConfirmed { get; set; }
        public Event<IOrderRejected> OrderRejected { get; set; }
        public State AwaitingConfirmation { get; set; }
    }
}
