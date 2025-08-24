using DigitalAwareness.Data;
using DigitalAwareness.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DigitalAwareness.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var isAdmin = User.IsInRole("Admin");

            if (isAdmin)
            {
                var allUsers = await _context.Users.OrderByDescending(u => u.CreatedDate).ToListAsync();
                ViewBag.IsAdmin = true;
                return View(allUsers);
            }
            else
            {
                var user = await _context.Users.FindAsync(userId);
                ViewBag.IsAdmin = false;
                return View(new List<User> { user! });
            }
        }

        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}

