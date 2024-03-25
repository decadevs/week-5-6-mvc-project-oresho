using System.ComponentModel.DataAnnotations;

namespace ResumePortal.Models.ViewModels
{
    public class WorkHistoryViewModel
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateOnly StartDate { get; set; }
        public bool IsStillWorkingHere { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
