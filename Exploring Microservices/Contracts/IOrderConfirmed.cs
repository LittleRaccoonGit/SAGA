namespace Contracts
{
    public interface IOrderConfirmed
    {
        Guid OrderId { get; set; } 
        string History { get; set; }
    }
}
