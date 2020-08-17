using System;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class User
    {
        public Guid Id { get; set; }
        // [JsonIgnore]
        // public string Email { get; set; }
        // [JsonIgnore]
        // public string Password { get; set; }
        // [JsonIgnore]
        // public bool ConfirmedEmail { get; set; }
        public DateTime RegisterDate { get; set; }

        public string Nick { get; set; }
        // public string Region { get; set; }
        // public string ClanName { get; set; }
        // public string Discord { get; set; }
    }
}