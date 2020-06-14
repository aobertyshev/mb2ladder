using System;

namespace MBIILadder.Shared.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Nick { get; set; }
        public string Region { get; set; }
        public string ClanName { get; set; }
        public string Discord { get; set; }
    }
}