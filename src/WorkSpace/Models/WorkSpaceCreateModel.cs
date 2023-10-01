using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WorkSpace.Models
{
    public class WorkSpaceCreateModel
    {
        [Required]
        [JsonProperty("workSpaceName")]
        public string WorkSpaceName { get; set; }
    }
}
