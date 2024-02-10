using Askify.Models;
using Askify.Services.IServices;
using Askify.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Askify.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEnduserService _enduserService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<AppRole> roleManager,
                                 IEnduserService enduserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _enduserService = enduserService;
        }
        public IActionResult Index()
        {
            var allUsers = _userManager.Users.AsEnumerable();
            return View(allUsers);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Registeration");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(UserRegisterationVM newUser)
        {
            if (ModelState.IsValid)
            {

                var result = await _enduserService.Add(newUser, _userManager);
                if (result.Item2.Succeeded && result.Item1 != null)
                {
                    await _signInManager.SignInAsync(result.Item1, isPersistent: false);
                    return RedirectToAction("tomyprofile", "enduser");
                }
                foreach(var error in result.Item2.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Registeration", newUser);
        }

        //[HttpGet]
        //public IActionResult RegisterUserInRole()
        //{
        //    ViewBag.roles = new SelectList(_roleManager.Roles, "Id", "Name");

        //    return View();
        //}
        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> RegisterUserInRole(UserRegisterationVM newUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var mapper = AutomapperConfig.InitializeAutoMapper();
        //        var user = mapper.Map<ApplicationUser>(newUser);

        //        var result = await _userManager.CreateAsync(user, newUser.Password);
        //        if (result.Succeeded)
        //        {
        //            var role = await _roleManager.FindByIdAsync(newUser.RoleId.ToString());
        //            var RoleAddedRes = await _userManager.AddToRoleAsync(user, role.Name);
        //            if (RoleAddedRes.Succeeded)
        //                return Ok("Role Added");
        //            return View();
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }

        //    return View();
        //}
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(UserLoginVM newUser)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByNameAsync(newUser.Username);
                if (appUser == null)
                {
                    ModelState.AddModelError("", "username is invalid");
                    return RedirectToAction("Login", newUser);
                }
                var result = await _userManager.CheckPasswordAsync(appUser, newUser.Password);
                if (result == false)
                {
                    ModelState.AddModelError("", "password is invalid");
                    return RedirectToAction("Login", newUser);
                }

                await _signInManager.SignInAsync(appUser, newUser.RememberMe);
                return RedirectToAction("tomyprofile", "enduser");
            }
            ModelState.AddModelError("", "username and password are invalid");
            return RedirectToAction("Login", newUser);
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            if (User.Identity.IsAuthenticated == false)
                return RedirectToAction(nameof(Login));
            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null)
                return RedirectToAction(nameof(Login));

            var editUserModel = 
                new EditVM { UserName = appUser?.UserName };

            return View(editUserModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditVM newUser)
        {
            if (ModelState.IsValid == false)
            {
                foreach(var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View(newUser);
            }
            var savedUser = await _userManager.GetUserAsync(User);
            //edit in enduser ************
            savedUser.UserName = newUser.UserName;

            var result = await _userManager.UpdateAsync(savedUser);
            if (result.Succeeded)
            {
                _enduserService.UpdateUserName(savedUser.EndUserId, newUser.UserName);
                await _signInManager.RefreshSignInAsync(savedUser);
                return RedirectToAction("index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(newUser);
        }
        [HttpGet]
        public async Task<IActionResult> EditPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(EditPasswordVM? password)
        {
            if (ModelState.IsValid == false)
            {
                foreach (var item in ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View();
            }
            var savedUser = await _userManager.GetUserAsync(User);
            if (savedUser == null)
            {
                ModelState.AddModelError("", "try again");
                return View();
            }
            var result = await _userManager.ChangePasswordAsync(savedUser, password.OldPassword, password.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("index", "Home");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }
    }
}
