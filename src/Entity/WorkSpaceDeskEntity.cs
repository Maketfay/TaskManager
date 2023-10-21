using Infrastructure.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class WorkSpaceDeskEntity: IWorkSpaceDesk
    {
        public Guid Id { get; set; }

        [ForeignKey("WorkSpace")]
        public Guid WorkSpaceFk { get; set; }
        public IWorkSpace WorkSpace { get; set; }

        [ForeignKey("Desk")]
        public Guid DeskFk { get; set; }
        public IDesk Desk { get; set; }
    }
}
