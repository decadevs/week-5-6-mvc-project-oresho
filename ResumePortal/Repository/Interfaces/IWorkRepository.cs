using ResumePortal.Models.Entities;

namespace ResumePortal.Repository.Interfaces
{
    public interface IWorkRepository
    {
        Task CreateAsync(WorkHistoryEntry workHistoryEntry);
        WorkHistoryEntry FindById(Guid id);
        Task DeleteAsync(WorkHistoryEntry workHistoryEntry);
        Task UpdateAsync(WorkHistoryEntry workHistoryEntry);
    }
}
