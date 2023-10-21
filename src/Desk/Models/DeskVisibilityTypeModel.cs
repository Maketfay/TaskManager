using Newtonsoft.Json;

namespace Desk.Models
{
    public class DeskVisibilityTypeModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
