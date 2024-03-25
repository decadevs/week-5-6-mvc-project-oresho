﻿using Microsoft.EntityFrameworkCore;
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

        public Task<IEnumerable<AppUser>> GetAllAppUsersAsync()
        {
            return GetAllAsync();
        }

        public AppUser GetAppUser()
        {
            return FindByCondition(x => x.Id != null)
                .Include(x => x.Address)
                .Include(x => x.WorkHistory)
                .FirstOrDefault();
        }
    }
}