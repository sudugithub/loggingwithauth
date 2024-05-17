using System.Text.Json.Serialization;

namespace Data.Domain
{
    public class User : AbstractEntity
    {
        public string? Name { get; set; }
        
        public required string Email { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
    }
}
