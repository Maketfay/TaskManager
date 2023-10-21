using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Desk.Models
{
    public class DeskCreateModel
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("deskVisibilityTypeCode")]
        public string DeskVisibilityTypeCode { get; set; }

        [Required]
        [JsonProperty("workSpaceId")]
        public Guid WorkSpaceId { get; set; }
    }
}
