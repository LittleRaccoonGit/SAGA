namespace Contracts
{
    public interface IOrderSubmited
    {
        Guid OrderId { get; set; }
        string Name { get; set; }
        string History { get; set; }
    }
}
