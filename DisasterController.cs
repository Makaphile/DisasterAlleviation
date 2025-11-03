using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlleviationApp.Controllers
{
    public class DisasterController : Controller
    {
        private readonly IDisasterRepository _disasterRepository;

        public DisasterController(IDisasterRepository disasterRepository)
        {
            _disasterRepository = disasterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var incidents = await _disasterRepository.GetAllIncidentsAsync();
            return View(incidents);
        }

        [HttpGet]
        public IActionResult Report()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Report(DisasterIncident incident)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                incident.ReportedByUserId = userId.Value;
                await _disasterRepository.CreateIncidentAsync(incident);

                TempData["SuccessMessage"] = "Incident reported successfully!";
                return RedirectToAction("Index");
            }

            return View(incident);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var incident = await _disasterRepository.GetIncidentByIdAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }
    }
}