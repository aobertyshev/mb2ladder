using System;

namespace Shared.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        // public DateTime Date { get; set; }
        // public DateTime DateCreated { get; set; }
        // public DateTime DateUpdated { get; set; }
        // public string[] Maps { get; set; }
        // public string Score { get; set; }
        public string[] Players { get; set; }
        public string Password { get; set; }
        public MatchState MatchState { get; set; } 
        public string ServerIp { get; set; }
    }
}