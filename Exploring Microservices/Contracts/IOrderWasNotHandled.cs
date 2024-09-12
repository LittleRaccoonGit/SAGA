namespace Contracts
{
    public interface IOrderWasNotHandled
    {
        Guid OrderId { get; set; }
        string History { get; set; }
    }
}
