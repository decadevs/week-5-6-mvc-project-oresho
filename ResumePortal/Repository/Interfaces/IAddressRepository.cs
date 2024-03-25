using ResumePortal.Models.Entities;

namespace ResumePortal.Repository.Interfaces
{
    public interface IAddressRepository
    {
        Task CreateAsync(Address address);
        Address FindById(Guid id);
    }
}
