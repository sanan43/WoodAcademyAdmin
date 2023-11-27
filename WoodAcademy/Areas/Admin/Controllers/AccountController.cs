using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WoodAcademy.Areas.Admin.ViewModels.Auth;
using WoodAcademy.Models.Auth;

namespace WoodAcademy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            AppUser Newuser = new AppUser()
            {
                Fullname = registerVM.FullName,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
            };
           IdentityResult RegisterResult = await _userManager.CreateAsync(Newuser, registerVM.Password);
            if (!RegisterResult.Succeeded) {

                foreach (IdentityError error in RegisterResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }
            //qeydiyyat olarken rol vermek

           //IdentityResult Roleresult = await _userManager.AddToRoleAsync(Newuser, UserRoles.Admin.ToString());
           // if (!Roleresult.Succeeded)
           // {

           //     foreach (IdentityError error in Roleresult.Errors)
           //     {
           //         ModelState.AddModelError("", error.Description);
           //     }
           //     return View(registerVM);
           // }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login,string? ReturnUrl)
        {
            if (!ModelState.IsValid) return View(login);
            AppUser user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "email veya password yanlisdir");
                return View(login);
               
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!signInResult.Succeeded) 
            {
                ModelState.AddModelError("", "email veya password yanlisdir");
                return View(login);
            }
            await _signInManager.SignInAsync(user, login.RememberMe);
            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }
      
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        //sqle rol add etmek

        //public async Task<IActionResult> AddRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(UserRoles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name=role.ToString()});
        //        }
        //    }
        //    return Json("Ok");
           
        //}

        public IActionResult AccessDenied()
        {
            return View();
        }

        enum UserRoles
        {
            Admin,
            User
        }

    }
}
