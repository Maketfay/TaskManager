namespace Infrastructure.Entity
{
    public interface IUser
    {
        Guid Id { get; set; }
        string Name { get; set; }
        public string PasswordHash { get; set; }
    }
}
