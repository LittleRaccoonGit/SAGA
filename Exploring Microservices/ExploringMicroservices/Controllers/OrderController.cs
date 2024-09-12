using Microsoft.AspNetCore.Mvc;
using Contracts;
using MassTransit;
using ExploringMicroservices.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ExploringMicroservices.Controllers
{
    [ApiController]
    [Route("controller")]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IPublishEndpoint publishEndpoint,
                                ILogger<OrdersController> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> PostOrder(string name)
        {
                var id = Guid.NewGuid();
            await _publishEndpoint.Publish<IOrderSubmited>(new
            {
                OrderId = id,
                Name = name,
                History = "Отправили запрос на подтверждение " + DateTime.Now.ToString() + (char)13 + (char)10
            });

                return Accepted(new { id });
            

        }
    }
}
