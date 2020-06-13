using System;

namespace MBIILadder.Shared.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ConfirmedEmail { get; set; }
    }
}