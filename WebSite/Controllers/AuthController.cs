using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Check if user is already logged in via session
            if (HttpContext.Session.GetString("IsAuthenticated") == "true")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Simple authentication logic - replace with actual Fanex.NAP.SignIn
                if (IsValidUser(model.Username, model.Password))
                {
                    // Set session variables
                    HttpContext.Session.SetString("IsAuthenticated", "true");
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetString("UserRole", "User");
                    HttpContext.Session.SetString("LoginTime", DateTime.UtcNow.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Login failed: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private bool IsValidUser(string username, string password)
        {
            // Simple validation - replace with actual Fanex.NAP.SignIn implementation
            // For demo purposes, accept any non-empty username/password
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
} 