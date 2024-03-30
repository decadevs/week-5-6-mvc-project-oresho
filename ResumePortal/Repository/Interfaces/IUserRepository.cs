using ResumePortal.Models.Entities;

namespace ResumePortal.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAppUsersAsync();
        AppUser GetAppUser();
        IQueryable<AppUser> FindAppUserByName(string name);
        Task CreateAsync(AppUser appUser);
        Task UpdateAsync(AppUser appUser);
        int Count();
    }
}
