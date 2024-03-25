using ResumePortal.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResumePortal.Models.ViewModels
{
    public class AddWorkHistoryViewModel
    {
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Job Description is required")]
        public string JobDescription { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "IsStillWorkingHere is required")]
        public bool IsStillWorkingHere { get; set; }
        public DateOnly EndDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
