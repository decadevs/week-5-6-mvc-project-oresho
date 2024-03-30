using Microsoft.AspNetCore.Mvc;
using ResumePortal.Models.ViewModels;
using ResumePortal.Services.User;
using ResumePortal.Services.Work;

namespace ResumePortal.Controllers
{
    public class WorkController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWorkService _workService;
        public WorkController(IUserService userService, IWorkService workService)
        {
            _userService = userService;
            _workService = workService;
        }
        public IActionResult AddWorkHistory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWorkHistory(AddWorkHistoryViewModel addWorkHistoryViewModel)
        {
            _userService.AddWorkEntry(addWorkHistoryViewModel);
            return RedirectToAction("Profile", "User");
        }

        public IActionResult JobDetails(Guid id)
        {
            var viewModel = _workService.GetJobDetail(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _workService.DeleteAsync(id);
            return RedirectToAction("Profile", "User");
        }

        public IActionResult Update(Guid id)
        {
            var viewModel = _workService.GetJobDetail(id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkHistoryViewModel workHistoryViewModel)
        {
            await _workService.UpdateAsync(workHistoryViewModel);
            return RedirectToAction("Profile", "User");
        }
    }
}
