using Infrastructure.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class WorkSpaceUserEntity: IWorkSpaceUser
    {
        public Guid Id { get; set; }

        [ForeignKey("WorkSpace")]
        public Guid WorkSpaceFk { get; set; }
        public IWorkSpace WorkSpace { get; set; }

        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }
    }
}
