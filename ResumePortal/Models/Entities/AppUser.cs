using System.ComponentModel.DataAnnotations;

namespace ResumePortal.Models.Entities
{
    public class AppUser
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }
        public string Summary { get; set; }
        public string? PhotoUrl { get; set; }
        public Guid? AddressId { get; set; }
        public Address Address { get; set; }
        public List<WorkHistoryEntry> WorkHistory { get; set; } = new List<WorkHistoryEntry>();
    }
}
