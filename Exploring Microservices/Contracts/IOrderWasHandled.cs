namespace Contracts
{
    public interface IOrderWasHandled
    {
        Guid OrderId { get; set; }
        string History { get; set; }
    }
}
