using Newtonsoft.Json;

namespace MBIILadder.WebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Region { get; set; }
        public bool ConfirmedEmail { get; set; }
        public string ClanName { get; set; }
    }
}