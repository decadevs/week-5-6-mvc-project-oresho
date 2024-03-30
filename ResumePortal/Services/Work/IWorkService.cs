using ResumePortal.Models.ViewModels;

namespace ResumePortal.Services.Work
{
    public interface IWorkService
    {
        WorkHistoryViewModel GetJobDetail(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(WorkHistoryViewModel workHistoryViewModel);
    }
}
