using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumePortal.Models.ViewModels;
using ResumePortal.Services.Email;
using ResumePortal.Services.User;

namespace ResumePortal.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;  
            _emailService = emailService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddUserViewModel addUserViewModel)
        {
            await _userService.CreateAsync(addUserViewModel);
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            var resume = _userService.GetResume();
            return View(resume);
        }

        [Authorize]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Contact(string emailText)
        {
            var user = _userService.GetUser();
            await _emailService.SendEmailAsync(user.Email,"Inquiry from Website" ,emailText);
            return View(true);
        }

        [Authorize]
        public IActionResult Update()
        {
            var user = _userService.GetUserForUpdate();
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(AddUserViewModel model)
        {
            await _userService.UpdateAsync(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
