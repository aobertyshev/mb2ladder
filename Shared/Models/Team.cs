using System;

namespace Shared.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public Guid[] UserIds { get; set; }
    }
}