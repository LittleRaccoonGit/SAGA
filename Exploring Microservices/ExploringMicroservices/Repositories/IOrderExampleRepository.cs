using Contracts;
using ExploringMicroservices.Models;

namespace ExploringMicroservices.Repositories
{
    public interface IOrderExampleRepository
    {
        Task<OrderExample> GetAsync(Guid id);
    }
}
