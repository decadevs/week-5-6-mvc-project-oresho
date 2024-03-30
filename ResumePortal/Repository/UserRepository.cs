using Microsoft.EntityFrameworkCore;
using ResumePortal.Data;
using ResumePortal.Models.Entities;
using ResumePortal.Repository.Interfaces;

namespace ResumePortal.Repository
{
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public IQueryable<AppUser> FindAppUserByName(string name)
        {
            return FindByCondition(x => x.Name == name);
        }

        public async Task<IEnumerable<AppUser>> GetAllAppUsersAsync()
        {
            return await GetAllAsync();
        }

        public AppUser GetAppUser()
        {
            return FindByCondition(x => x.Email == "somuyiwaoreoluwa@gmail.com")
                .Include(x => x.Address)
                .Include(x => x.WorkHistory)
                .FirstOrDefault();
        }
    }
}
