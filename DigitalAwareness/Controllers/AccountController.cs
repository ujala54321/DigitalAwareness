using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DigitalAwareness.Data;
using DigitalAwareness.Models;
using DigitalAwareness.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace DigitalAwareness.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPassword(model.Password);
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == hashedPassword);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                        new Claim("ReferralCode", user.ReferralCode)
                    };

                    if (user.IsAdmin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.States = await _context.States.ToListAsync();
            ViewBag.Designations = new List<string>
            {
                "District Coordinator",
                "Block Coordinator",
                "Panchayat Coordinator",
                "District Member",
                "Block Member",
                "Panchayat Member"
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    ViewBag.States = await _context.States.ToListAsync();
                    ViewBag.Designations = new List<string>
                    {
                        "District Coordinator", "Block Coordinator", "Panchayat Coordinator",
                        "District Member", "Block Member", "Panchayat Member"
                    };
                    return View(model);
                }

                var user = new User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FatherName = model.FatherName,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    Designation = model.Designation,
                    BirthDate = model.BirthDate,
                    Village = model.Village,
                    Post = model.Post,
                    Block = model.Block,
                    City = model.City,
                    State = model.State,
                    Pincode = model.Pincode,
                    Password = HashPassword(model.Password),
                    CreatedDate = DateTime.Now
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Generate referral code after getting the ReferralNumber
                await _context.Entry(user).ReloadAsync();
                user.ReferralCode = $"DA{user.ReferralNumber:D3}";
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Registration successful! Your Referral ID is: {user.ReferralCode}";
                return RedirectToAction("Login");
            }

            ViewBag.States = await _context.States.ToListAsync();
            ViewBag.Designations = new List<string>
            {
                "District Coordinator", "Block Coordinator", "Panchayat Coordinator",
                "District Member", "Block Member", "Panchayat Member"
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<JsonResult> GetCities(int stateId)
        {
            var cities = await _context.Cities
                .Where(c => c.StateId == stateId)
                .Select(c => new { value = c.Name, text = c.Name })
                .ToListAsync();

            return Json(cities);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}

