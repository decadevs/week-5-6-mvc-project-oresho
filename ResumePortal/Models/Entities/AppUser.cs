using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ResumePortal.Models.Entities
{
    public class AppUser : IdentityUser
    {
        [Key]
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public int? Age { get; set; }
        public char? Gender { get; set; }
        public override string? Email { get; set; }
        public override string? PhoneNumber { get; set; }
        public string? Job { get; set; }
        public string? Summary { get; set; }
        public string? PhotoUrl { get; set; }
        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }
        public List<WorkHistoryEntry> WorkHistory { get; set; } = new List<WorkHistoryEntry>();
    }
}
