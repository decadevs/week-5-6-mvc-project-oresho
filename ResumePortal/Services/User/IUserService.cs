using ResumePortal.Models.Entities;
using ResumePortal.Models.ViewModels;

namespace ResumePortal.Services.User
{
    public interface IUserService
    {
        Task Create(AddUserViewModel viewModel);
        UserViewModel GetUser();
        Task AddWorkEntry(AddWorkHistoryViewModel viewModel);
        ResumeViewModel GetResume();
        AddUserViewModel GetUserForUpdate();
        void Update(AddUserViewModel model);
    }
}
