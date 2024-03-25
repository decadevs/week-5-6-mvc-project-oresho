using Microsoft.EntityFrameworkCore;
using ResumePortal.Data;
using ResumePortal.Repository.Interfaces;
using System.Linq.Expressions;

namespace ResumePortal.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _applicationDbContext;

        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public int Count()
        {
            return _applicationDbContext.Set<T>().Count();
        }

        public async Task CreateAsync(T entity)
        {
            await _applicationDbContext.Set<T>().AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
            _applicationDbContext.SaveChangesAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _applicationDbContext.Set<T>().Where(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _applicationDbContext.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _applicationDbContext.Set<T>().Update(entity);
            _applicationDbContext.SaveChangesAsync();
        }
    }
}
