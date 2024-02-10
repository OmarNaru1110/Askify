using Askify.Models;
using Askify.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Askify.Controllers
{
    public class EnduserController : Controller
    {
        private readonly IEnduserService _enduserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IQuestionService _questionService;

        public EnduserController(
            IEnduserService enduserService,
            UserManager<AppUser> userManager,
            IAccountService accountService,
            IQuestionService questionService
            ) 
        {
            _enduserService = enduserService;
            _userManager = userManager;
            _accountService = accountService;
            _questionService = questionService;
        }
        public async Task<IActionResult> DeleteQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound("question u want to delete wasn't found");
            }
            _questionService.Delete(id.Value);
            return RedirectToAction("ToInbox");
        }
        public async Task<IActionResult> AskQuestion(string? text, string? Anonymous)
        {
            string ? receiverStr = TempData["receiverId"].ToString();
            var result = int.TryParse(receiverStr, out int receiverId);
            if (result == false)
                return BadRequest("something went wrong");
            
            if(_enduserService.SendQuestion(text,Anonymous,receiverId) == false)
            {
                return BadRequest("something went wrong");
            }
            
            TempData["questionSent"] = "true";

            return RedirectToAction("ToAnotherProfile", new { endUserId = receiverId });
        }
        public async Task<IActionResult> ToInbox()
        {
            var inbox = _enduserService.GetInbox();
            return View("inbox",inbox);
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

            TempData["receiverId"] = endUserId;

            if (TempData["questionSent"] != null)
                ViewBag.questionSent = "true";

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
