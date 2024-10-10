using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoShop.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<User> userManager;

        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }
    }
}
