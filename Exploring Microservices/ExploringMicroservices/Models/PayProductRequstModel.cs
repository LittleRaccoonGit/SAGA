using Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ExploringMicroservices.Models
{
    public class PayProductRequstModel : PaymentRequest
    {
        public int Count { get; set; }

        public decimal Amount { get; set; }
    }
}
