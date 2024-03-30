using ResumePortal.Models.Entities;
using ResumePortal.Models.ViewModels;

namespace ResumePortal.Services.User
{
    public interface IUserService
    {
        Task CreateAsync(AddUserViewModel viewModel);
        UserViewModel GetUser();
        Task AddWorkEntry(AddWorkHistoryViewModel viewModel);
        ResumeViewModel GetResume();
        AddUserViewModel GetUserForUpdate();
        Task UpdateAsync(AddUserViewModel model);
    }
}
