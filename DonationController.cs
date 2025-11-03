using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlleviationApp.Controllers
{
    public class DonationController : Controller
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IDisasterRepository _disasterRepository;

        public DonationController(IDonationRepository donationRepository, IDisasterRepository disasterRepository)
        {
            _donationRepository = donationRepository;
            _disasterRepository = disasterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var donations = await _donationRepository.GetAllDonationsAsync();
            return View(donations);
        }

        [HttpGet]
        public async Task<IActionResult> Donate()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.DisasterIncidents = await _disasterRepository.GetActiveIncidentsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Donate(Donation donation)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                donation.DonatedByUserId = userId.Value;
                await _donationRepository.CreateDonationAsync(donation);

                TempData["SuccessMessage"] = "Thank you for your donation!";
                return RedirectToAction("Index");
            }

            ViewBag.DisasterIncidents = await _disasterRepository.GetActiveIncidentsAsync();
            return View(donation);
        }

        [HttpGet]
        public async Task<IActionResult> MyDonations()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var donations = await _donationRepository.GetDonationsByUserAsync(userId.Value);
            return View(donations);
        }
    }
}