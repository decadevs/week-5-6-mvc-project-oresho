using System.ComponentModel.DataAnnotations;

namespace ResumePortal.Models.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
