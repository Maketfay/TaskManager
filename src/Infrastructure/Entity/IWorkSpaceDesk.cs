namespace Infrastructure.Entity
{
    public interface IWorkSpaceDesk
    {
        Guid Id { get; set; }
        IWorkSpace WorkSpace { get; set; } //Unique
        IDesk Desk { get; set; } 
    }
}
