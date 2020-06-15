using System;
using System.Collections.Generic;

namespace MBIILadder.Shared.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public IList<Guid> PlayerIds { get; set; }
    }
}