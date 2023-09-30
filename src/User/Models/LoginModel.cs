using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace User.Models
{
    public class LoginModel
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
