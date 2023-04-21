using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Ciber.EntityFramework.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Ciber.Models;
using Microsoft.EntityFrameworkCore;

namespace Ciber.Controllers
{
    public class LoginController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CiberDbContext _ciberDbContext;

        public LoginController(RoleManager<IdentityRole> roleMgr, CiberDbContext ciberDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _roleManager = roleMgr;
            _userManager = userManager;
            _signInManager = signInManager;
            _ciberDbContext = ciberDbContext;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel input)
        {
            var getall = _userManager.Users;
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() ==
                 input.Username.ToLower() && user.PasswordHash == input.Password);
            if (user != null)
            {
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("ListViewProduct", "Home");
            }
            ViewBag.Error = "Not found User";
            return View("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}
