namespace Infrastructure.Entity
{
    public interface IDesk
    {
        Guid Id { get; set; }
        string Name { get; set; }
        IDeskVisibilityType DeskVisibilityType { get; set; }
        IUser User { get; set; }
    }
}
