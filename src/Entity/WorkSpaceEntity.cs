using Infrastructure.Entity;

namespace Entity
{
    public class WorkSpaceEntity: IWorkSpace
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
