using Newtonsoft.Json;

namespace Desk.Models
{
    public class DeskModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
