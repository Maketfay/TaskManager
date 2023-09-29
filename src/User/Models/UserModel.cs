using Newtonsoft.Json;

namespace User.Models
{
    public class UserModel
    {
        [JsonProperty("userId")]
        public Guid Id { get; set; }
    }
}
