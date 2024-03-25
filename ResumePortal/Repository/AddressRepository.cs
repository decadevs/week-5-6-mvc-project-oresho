using ResumePortal.Data;
using ResumePortal.Models.Entities;
using ResumePortal.Repository.Interfaces;

namespace ResumePortal.Repository
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
        }

        public Address FindById(Guid id)
        {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }
    }
}
