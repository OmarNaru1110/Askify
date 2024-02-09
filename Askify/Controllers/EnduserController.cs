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
        private readonly IAccountService _accountService;

        public EnduserController(
            IEnduserService enduserService,
            UserManager<AppUser> userManager,
            IAccountService accountService
            ) 
        {
            _enduserService = enduserService;
            _userManager = userManager;
            _accountService = accountService;
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
                return NotFound("user not found");
            }

            var user = _enduserService.GetById(endUserId);
            if (user == null)
            {
                return NotFound("user not found");
            }
            var userDetails = _enduserService.GetDetails(user.Id);
            userDetails.EndUser = user;
            var isFollowing = _enduserService.CheckIsFollowing(user.Id);
            if (isFollowing == false)
            {
                ViewData["isFollowing"] = "Follow";
            }
            else
            {
                ViewData["isFollowing"] = "Unfollow";
            }

            return View("AnotherProfile", userDetails);
        }
        public async Task<IActionResult> ManageFollow(int? endUserId, string? isFollowing)
        {
            if(isFollowing == null)
            {
                return NotFound("something went wrong");
            }
            if (endUserId == null)
            {
                return NotFound("user not found");
            }
            if(isFollowing == "Follow")
            {
                //add
                _enduserService.AddFollowing(endUserId);
            }
            else
            {
                //remove
                _enduserService.RemoveFollowing(endUserId);
            }
            return RedirectToAction("ToAnotherProfile", new { endUserId = endUserId });
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
