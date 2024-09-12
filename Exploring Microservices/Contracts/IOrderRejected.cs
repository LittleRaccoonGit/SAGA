namespace Contracts
{
    public interface IOrderRejected
    {
        Guid OrderId { get; set; }
        string History { get; set; }
    }
}
