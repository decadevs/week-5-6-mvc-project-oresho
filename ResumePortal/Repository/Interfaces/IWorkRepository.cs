using ResumePortal.Models.Entities;

namespace ResumePortal.Repository.Interfaces
{
    public interface IWorkRepository
    {
        Task CreateAsync(WorkHistoryEntry workHistoryEntry);
        WorkHistoryEntry FindById(Guid id);
        void Delete(WorkHistoryEntry workHistoryEntry);
        void Update(WorkHistoryEntry workHistoryEntry);
    }
}
