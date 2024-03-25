using ResumePortal.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResumePortal.Models.ViewModels
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Occupation is required")]
        public string Job { get; set; }
        [Required(ErrorMessage = "Summary is required")]
        public string Summary { get; set; }
        public IFormFile? PhotoUrl { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
