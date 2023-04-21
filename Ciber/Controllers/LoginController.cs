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
    [Authorize]
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
                var getRols = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, user.UserName),
                    };
                foreach (var r in getRols)
                {
                    claims.Add(new Claim(ClaimTypes.Role, r));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });
               
                return RedirectToAction("Index","Home");
            }
            ViewBag.Error = "Not found User";
            return View("Login");

        }
    }
}
