namespace Infrastructure.Entity
{
    public interface IWorkSpaceUser
    {
        Guid Id { get; set; }
        IWorkSpace WorkSpace { get; set; }
        IUser User { get; set; }
    }
}
