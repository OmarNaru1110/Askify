using Askify.Models;
using Askify.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Askify.Controllers
{
    public class EnduserController : Controller
    {
        private readonly IEnduserService _enduserService;
        private readonly UserManager<AppUser> _userManager;

        public EnduserController(
            IEnduserService enduserService,
            UserManager<AppUser> userManager
            ) 
        {
            _enduserService = enduserService;
            _userManager = userManager;
        }
        public async Task<IActionResult> ToMyProfile()
        {
            var appUser = await _userManager.GetUserAsync(User);
            if(appUser == null )
            {
                return NotFound("user not found");
            }
            var user = _enduserService.GetById(appUser.EndUserId);
            if (user == null)
            {
                return NotFound("user not found");
            }
            var userDetails = _enduserService.GetDetails(user.Id);
            userDetails.EndUser = user;
            return View("Profile",userDetails);
        }
        public async Task<IActionResult> ToAnotherProfile(int? endUserId)
        {
            if(endUserId == null)
            {
                return NotFound("user not fount");
            }

            var user = _enduserService.GetById(endUserId);
            if (user == null)
            {
                return NotFound("user not found");
            }
            var userDetails = _enduserService.GetDetails(user.Id);
            userDetails.EndUser = user;
            return View("AnotherProfile", userDetails);
        }
        public async Task<IActionResult> GetFollowing(int endUserId)
        {
            var following = _enduserService.GetFollowingList(endUserId);
            return View(following);
        }
        public async Task<IActionResult> GetFollowers(int endUserId)
        {
            var followers = _enduserService.GetFollowersList(endUserId);
            return View(followers);
        }

    }
}
