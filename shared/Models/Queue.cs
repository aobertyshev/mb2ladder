using System;
using System.Collections.Generic;

namespace MBIILadder.Shared.Models
{
    public class Queue
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, bool> PlayerIds { get; set; }
    }
}