using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlleviationApp.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IDisasterRepository _disasterRepository;

        public VolunteerController(IVolunteerRepository volunteerRepository, IDisasterRepository disasterRepository)
        {
            _volunteerRepository = volunteerRepository;
            _disasterRepository = disasterRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Volunteer volunteer)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                volunteer.UserId = userId.Value;
                await _volunteerRepository.RegisterVolunteerAsync(volunteer);

                TempData["SuccessMessage"] = "Volunteer registration successful!";
                return RedirectToAction("Tasks");
            }

            return View(volunteer);
        }

        [HttpGet]
        public async Task<IActionResult> Tasks()
        {
            var tasks = await _volunteerRepository.GetAvailableTasksAsync();
            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> TaskDetails(int id)
        {
            var task = await _volunteerRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> AssignToTask(int taskId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var volunteer = await _volunteerRepository.GetVolunteerByUserIdAsync(userId.Value);
            if (volunteer == null)
            {
                TempData["ErrorMessage"] = "Please register as a volunteer first.";
                return RedirectToAction("Register");
            }

            var assignment = new VolunteerAssignment
            {
                VolunteerId = volunteer.Id,
                TaskId = taskId,
                AssignmentDate = DateTime.UtcNow,
                Status = "Assigned"
            };

            await _volunteerRepository.AssignVolunteerToTaskAsync(assignment);

            TempData["SuccessMessage"] = "Successfully assigned to task!";
            return RedirectToAction("MyAssignments");
        }

        [HttpGet]
        public async Task<IActionResult> MyAssignments()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var volunteer = await _volunteerRepository.GetVolunteerByUserIdAsync(userId.Value);
            if (volunteer == null)
            {
                return RedirectToAction("Register");
            }

            var assignments = await _volunteerRepository.GetVolunteerAssignmentsAsync(volunteer.Id);
            return View(assignments);
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(VolunteerTask task)
        {
            if (ModelState.IsValid)
            {
                await _volunteerRepository.CreateTaskAsync(task);
                TempData["SuccessMessage"] = "Task created successfully!";
                return RedirectToAction("Tasks");
            }
            return View(task);
        }
    }
}