using Infrastructure.Entity;

namespace Entity
{
    public class DeskVisibilityTypeEntity: IDeskVisibilityType
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}
