using System.Text.Json;
using System.Text.Json.Serialization;

namespace MBIILadder.WebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        [JsonIgnore]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Region { get; set; }
        [JsonIgnore]
        public bool ConfirmedEmail { get; set; }
        public string ClanName { get; set; }
    }
}