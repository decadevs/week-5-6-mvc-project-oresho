using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumePortal.Models.Entities
{
    public class WorkHistoryEntry
    {
        [Key]
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public bool IsStillWorkingHere { get; set; }
        public Guid AddressId { get; set; }
        public Address CompanyAddress { get; set; }

        [ForeignKey("AppUser")]
        public Guid AppUserId;
        public AppUser AppUser { get; set; }
    }
}
