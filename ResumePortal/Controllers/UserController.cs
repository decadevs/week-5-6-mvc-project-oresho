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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel addUserViewModel)
        {
            await _userService.Create(addUserViewModel);
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var resume = _userService.GetResume();
            return View(resume);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string emailText)
        {
            var task = _emailService.SendEmailAsync("Inquiry from Website" ,emailText);
            var result = task.IsCompletedSuccessfully;
            return View(result);
        }

        public IActionResult Update()
        {
            var user = _userService.GetUserForUpdate();
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(AddUserViewModel model)
        {
            _userService.Update(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
