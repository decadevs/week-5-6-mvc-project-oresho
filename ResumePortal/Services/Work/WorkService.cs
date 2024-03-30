using ResumePortal.Models.Entities;
using ResumePortal.Models.ViewModels;
using ResumePortal.Repository;
using ResumePortal.Repository.Interfaces;

namespace ResumePortal.Services.Work
{
    public class WorkService : IWorkService
    {
        private readonly IWorkRepository _workRepository;
        private readonly IAddressRepository _addressRepository;
        public WorkService(IWorkRepository workRepository, IAddressRepository addressRepository)
        {
            _workRepository = workRepository;
            _addressRepository = addressRepository;

        }

        public async Task DeleteAsync(Guid id)
        {
            var job = _workRepository.FindById(id);
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }
            await _workRepository.DeleteAsync(job);
        }

        public WorkHistoryViewModel GetJobDetail(Guid id)
        {
            var job = _workRepository.FindById(id);
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }
            var viewModel = MapToViewModel(job);

            return viewModel;
        }

        public async Task UpdateAsync(WorkHistoryViewModel workHistoryViewModel)
        {
            var job = _workRepository.FindById(workHistoryViewModel.Id);
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }
            Address address = _addressRepository.FindById(job.AddressId);

            address.Street = workHistoryViewModel.Street;
            address.City = workHistoryViewModel.City;
            address.State = workHistoryViewModel.State;
            address.Country = workHistoryViewModel.Country;

            job.CompanyName = workHistoryViewModel.CompanyName;
            job.JobDescription = workHistoryViewModel.JobDescription;
            job.JobTitle = workHistoryViewModel.JobTitle;
            job.StartDate = workHistoryViewModel.StartDate;
            job.EndDate = workHistoryViewModel.IsStillWorkingHere ? null : workHistoryViewModel.EndDate;
            job.IsStillWorkingHere = workHistoryViewModel.IsStillWorkingHere;
            job.CompanyAddress = address;

            await _workRepository.UpdateAsync(job);
        }

        private WorkHistoryViewModel MapToViewModel(WorkHistoryEntry workHistoryEntry)
        {
            Address address = _addressRepository.FindById(workHistoryEntry.AddressId);
            var viewModel = new WorkHistoryViewModel()
            {
                Id = workHistoryEntry.Id,
                CompanyName = workHistoryEntry.CompanyName,
                StartDate = workHistoryEntry.StartDate,
                EndDate = workHistoryEntry.IsStillWorkingHere ? null : workHistoryEntry.EndDate,
                IsStillWorkingHere = workHistoryEntry.IsStillWorkingHere,
                JobDescription = workHistoryEntry.JobDescription,
                JobTitle = workHistoryEntry.JobTitle,
                Street = address.Street,
                City = address.City,
                State = address.State,
                Country = address.Country
            };
            return viewModel;
        }
    }
}
