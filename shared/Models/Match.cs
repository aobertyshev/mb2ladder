using System;
using System.Collections.Generic;

namespace MBIILadder.Shared.Models
{
    public class Match
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public IList<string> Maps { get; set; }
        public string Score { get; set; }
    }
}