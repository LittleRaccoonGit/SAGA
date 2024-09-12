namespace Contracts
{
    public interface IConfirmationReqested
    {
        Guid OrderId { get; set; }
        string Name { get; set; }
        string History { get; set; }
    }
}
