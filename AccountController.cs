using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlleviationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user, string password)
        {
            if (await _userRepository.UserExistsAsync(user.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                // Hash password (in real app, use proper hashing)
                user.PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));

                await _userRepository.CreateUserAsync(user);

                // Set user session
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (await _userRepository.ValidateUserAsync(email, password))
            {
                var user = await _userRepository.GetUserByEmailAsync(email);

                // Set user session
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _userRepository.GetUserByIdAsync(userId.Value);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View(user);
        }
    }
}