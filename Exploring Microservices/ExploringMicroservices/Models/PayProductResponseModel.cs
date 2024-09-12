using Contracts;

namespace ExploringMicroservices.Models
{
    public class PayProductResponseModel : PaymentResponse
    {
        public string? ErrorMessage { get; set; }
    }
}
