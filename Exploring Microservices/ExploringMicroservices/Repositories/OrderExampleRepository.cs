using ExploringMicroservices.Models;

namespace ExploringMicroservices.Repositories
{
    public class OrderExampleRepository : IOrderExampleRepository
    {
        public Task<OrderExample> GetAsync(Guid id)
        {
            OrderExample Order = new OrderExample
            {
                OrderId = id,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Etalon",
                CustomerPhone = "099-088-65-99",
                CustomerEmail = "Etalon@Etalon.net",
                CustomerCity = "Odessa"
            };
            return Task.FromResult(Order);
        }
    }
}
