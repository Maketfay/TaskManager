using Newtonsoft.Json;

namespace WorkSpace.Models
{
    public class WorkSpaceResponseModel
    {
        [JsonProperty("workSpaceId")]
        public Guid WorkSpaceId { get; set; }
    }
}
