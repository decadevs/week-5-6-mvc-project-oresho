﻿using ResumePortal.Models.Entities;
using ResumePortal.Models.ViewModels;
using ResumePortal.Repository;
using ResumePortal.Repository.Interfaces;
using ResumePortal.Services.Image;

namespace ResumePortal.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IImageService _imageService;
        private readonly IAddressRepository _addressRepository;
        public UserService(IUserRepository userRepository, 
            IWorkRepository workRepository, 
            IImageService imageService, 
            IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _workRepository = workRepository;
            _imageService = imageService;
            _addressRepository = addressRepository;
        }

        public async Task AddWorkEntry(AddWorkHistoryViewModel viewModel)
        {
            var user = _userRepository.GetAppUser();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            Address address = new Address()
            {
                City = viewModel.City,
                State = viewModel.State,
                Country = viewModel.Country,
                Street = viewModel.Street,
            };

            WorkHistoryEntry workHistoryEntry = new WorkHistoryEntry() { 
                StartDate = viewModel.StartDate,
                EndDate = viewModel.IsStillWorkingHere ? null : viewModel.EndDate,
                CompanyName = viewModel.CompanyName,
                JobTitle = viewModel.JobTitle,
                JobDescription = viewModel.JobDescription,
                IsStillWorkingHere = viewModel.IsStillWorkingHere,
                CompanyAddress = address,
                AppUser = user
            };
            await _workRepository.CreateAsync(workHistoryEntry);
        }

        public async Task Create(AddUserViewModel viewModel)
        {
            int num = _userRepository.Count();
            if (num > 0)
            {
                throw new Exception("User has already been created");
            }
            Address address = new Address() { 
                City = viewModel.City,
                State = viewModel.State,
                Country = viewModel.Country,
                Street = viewModel.Street,
            };
            AppUser appUser = new AppUser() { 
                Name = viewModel.Firstname + " " + viewModel.Lastname,
                Age = viewModel.Age,
                Email = viewModel.Email,
                Job = viewModel.Job,
                Summary = viewModel.Summary,
                PhoneNumber = viewModel.PhoneNumber,
                PhotoUrl = _imageService.UploadImage(viewModel.PhotoUrl).Data,
                Gender = viewModel.Gender,
                Address = address,
            };
            await _userRepository.CreateAsync(appUser);
        }

        public ResumeViewModel GetResume()
        {
            var user = _userRepository.GetAppUser();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var workHistory = user.WorkHistory;
            var resume = workHistory.Select(MapToViewModel).ToList();
            var viewModel = new ResumeViewModel() { Resume = resume };
            return viewModel;
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

        public UserViewModel GetUser()
        {
            var user = _userRepository.GetAppUser();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            UserViewModel model = new UserViewModel() { 
                Name = user.Name,
                Age=user.Age,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Summary = user.Summary,
                Job = user.Job,
                PhotoUrl = user.PhotoUrl,
                State = user.Address.State,
                Country = user.Address.Country
            };
            return model;
        }
    }
}