using Infrastructure.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class DeskEntity: IDesk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("DeskVisibilityType")]
        public Guid DeskVisibilityTypeFk { get; set; }
        public IDeskVisibilityType DeskVisibilityType { get; set; }

        [ForeignKey("User")]
        public Guid UserFk { get; set; }
        public IUser User { get; set; }
    }
}
