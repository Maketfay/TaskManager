using Infrastructure.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class UserRefreshTokenEntity: IUserRefreshToken
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expired { get; set; }

        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }
    }
}
