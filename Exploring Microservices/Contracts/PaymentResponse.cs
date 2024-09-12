namespace Contracts
{
    public class PaymentResponse
    {
        public Guid OrderId { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
