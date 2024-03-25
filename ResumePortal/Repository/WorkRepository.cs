using ResumePortal.Data;
using ResumePortal.Models.Entities;
using ResumePortal.Repository.Interfaces;

namespace ResumePortal.Repository
{
    public class WorkRepository : RepositoryBase<WorkHistoryEntry>, IWorkRepository
    {
        public WorkRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public WorkHistoryEntry FindById(Guid id)
        {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }
    }
}
