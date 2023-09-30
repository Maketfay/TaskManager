namespace Infrastructure.Entity
{
    public interface IUserRefreshToken
    {
        Guid Id { get; set; }
        string RefreshToken { get; set; }
        DateTime Expired { get; set; }
        IUser User { get; set; }
    }
}
