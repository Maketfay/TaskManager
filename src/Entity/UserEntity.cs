using Infrastructure.Entity;

namespace Entity
{
    public class UserEntity: IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string PasswordHash { get; set; }
    }
}
