using Newtonsoft.Json;

namespace ChefsBook_UWP_App.Services.Models
{
    public class UserInfo
    {
        [JsonProperty(PropertyName = "sub")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string Picture { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
