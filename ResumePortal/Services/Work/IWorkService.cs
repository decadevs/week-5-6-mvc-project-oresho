using ResumePortal.Models.ViewModels;

namespace ResumePortal.Services.Work
{
    public interface IWorkService
    {
        WorkHistoryViewModel GetJobDetail(Guid id);
        void Delete(Guid id);
        void Update(WorkHistoryViewModel workHistoryViewModel);
    }
}
