using Askify.Models;
using Askify.Services;
using Askify.Services.IServices;
using Askify.ViewModels;
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
        private readonly IAnswerService _answerService;
        private readonly ITimelineService _timelineService;
        private readonly INotificationService _notificationService;
        private readonly IUserAnswerLikesService _userAnswerLikesService;

        public EnduserController(
            IEnduserService enduserService,
            UserManager<AppUser> userManager,
            IAccountService accountService,
            IQuestionService questionService,
            IAnswerService answerService,
            ITimelineService timelineService,
            INotificationService notificationService,
            IUserAnswerLikesService userAnswerLikesService
            ) 
        {
            _enduserService = enduserService;
            _userManager = userManager;
            _accountService = accountService;
            _questionService = questionService;
            _answerService = answerService;
            _timelineService = timelineService;
            _notificationService = notificationService;
            _userAnswerLikesService = userAnswerLikesService;
        }

        public async Task<IActionResult> GetWholeAnswer(int? parentQuestionId)
        {
            if (parentQuestionId == null)
                return NotFound("Something went wrong");

            var questions = _questionService.GetQuestionChildrenWithTheirAnswers(parentQuestionId);
            return View("WholeAnswer", questions);

        }
        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("ToTimeline");

            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> SearchAnswer(string? answerText, int? endUserId)
        {
            ViewBag.myAnswers = _answerService.SearchAnswers(answerText,endUserId);
            return View();
        }
        public async Task<IActionResult> SearchUser(string username)
        {
            var users = new List<EndUser>();
            users = _enduserService.Search(username);
            return View(users);
        }
        public async Task<IActionResult> ToTimeline(int page = 1)
        {
            var userId=_accountService.GetCurrentEndUserId();
            if (userId == null)
                return RedirectToAction("index", "home");

            //pagination
            int pageSize = HelperService.GetPageSize();
            int numberOfPages = HelperService.GetNumberOfPages();
            var timeline = _timelineService.GetFollowingAnswers(userId.Value, page, pageSize);
            var TotalAnswersCount = _timelineService.GetTimelineAnswersCount(userId.Value);

            ViewBag.myAnswers = timeline;
            ViewBag.pageNumber = page;
            ViewBag.pages = HelperService.Paginate(page, pageSize, numberOfPages, TotalAnswersCount);

            return View("Timeline");
        }
        public async Task<IActionResult> ToAnswer(int? answerId, int? notificationId)
        {
            if (answerId == null || notificationId == null)
                return NotFound("Answer not found");
            _notificationService.Edit(notificationId);
            var answer = _answerService.Edit(answerId);
            if (answer == null)
                return NotFound("Answer not found");

            var question = _questionService.GetQuestion(answer.QuestionId);

            if (question == null)
                return NotFound("Answer not found");

            var questions = new List<Question>();

            //i am the parent 
            if (question.ChildrenQuestions != null && question.ChildrenQuestions.Count!=0)
            {
                return RedirectToAction("GetWholeAnswer", new { parentQuestionId = question.Id });
            }
            //i am a child
            if(question.ParentQuestionId != null)
            {
                return RedirectToAction("GetWholeAnswer", new { parentQuestionId = question.ParentQuestionId});
            }

            return View(answer);
        }
        public async Task<IActionResult> ToNotifications()
        {
            var myId=_accountService.GetCurrentEndUserId();
            if (myId == null)
                return NotFound("something went wrong");

            var notifications = _notificationService.GetNotifications(myId.Value);
            
            return View("Notification",notifications);
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
        [HttpGet]
        public async Task<IActionResult> AnswerQuestion(int? questionId)
        {
            var question = _questionService.GetQuestion(questionId);
            if(question == null)
            {
                return BadRequest("something went wrong");
            }

            var questions = new List<Question>();

            if (question.ParentQuestionId != null)
            {
                questions = _questionService.GetQuestionChildrenWithTheirAnswers(question.ParentQuestionId);
            }
            else
            {
                questions.Add(question);
            }

            ViewBag.questionsList = questions;
            ViewBag.questionId = question.Id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnswerQuestion(AnswerQuestionVm answer)
        {
            var result = _answerService.Add(answer);
            return RedirectToAction("ToInbox");
        }
        public async Task<IActionResult> AskQuestion(
            string? text, string? anonymous, int? questionId, int? receiverId)
        {
            if (receiverId == null)
                return BadRequest("something went wrong");
            
            if(_questionService.SendQuestion(text,anonymous,receiverId.Value,questionId) == false)
            {
                return BadRequest("something went wrong");
            }
            
            TempData["questionSent"] = "true";

            return RedirectToAction("ToAnotherProfile", new { endUserId = receiverId });
        }
        public async Task<IActionResult> ToInbox()
        {
            var inbox = _questionService.GetInbox();
            return View("inbox",inbox);
        }
        public async Task<IActionResult> ToMyProfile(int page = 1)
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

            //pagination
            int pageSize = HelperService.GetPageSize();
            int numberOfPages = HelperService.GetNumberOfPages();
            var myAnswers = _answerService.GetUserAnswers(user.Id, page, pageSize);
            var TotalAnswersCount = _answerService.GetUserAnswersCount(user.Id);

            ViewBag.myAnswers = myAnswers;
            ViewBag.pageNumber = page;
            ViewBag.pages = HelperService.Paginate(page, pageSize, numberOfPages, TotalAnswersCount);

            return View("Profile",userDetails);
        }
        public async Task<IActionResult> ToAnotherProfile(int? endUserId, int page=1)
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

            //pagination
            int pageSize = HelperService.GetPageSize();
            int numberOfPages = HelperService.GetNumberOfPages();
            var myAnswers = _answerService.GetUserAnswers(user.Id, page, pageSize);
            var TotalAnswersCount = _answerService.GetUserAnswersCount(endUserId);

            ViewBag.myAnswers = myAnswers;
            ViewBag.pageNumber = page;
            ViewBag.pages = HelperService.Paginate(page, pageSize, numberOfPages, TotalAnswersCount);

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
        public JsonResult ManageLikes(int? answerId)
        {
            if (answerId == null)
            {
                return Json(new { success = false, errorMessage = "Invalid answerId" });
            }

            var userId = _accountService.GetCurrentEndUserId();
            if (userId == null)
            {
                return Json(new { success = false, errorMessage = "User not authenticated" });
            }

            // _userAnswerLikesService.Manage method returns the updated likes count
            var updatedLikesCount = _userAnswerLikesService.Manage(answerId.Value, userId.Value);

            // Return the updated likes count
            return Json(new { success = true, likesCount = updatedLikesCount });
        }

        public async Task<IActionResult> GetFollowers(int endUserId)
        {
            var followers = _enduserService.GetFollowersList(endUserId);
            return View(followers);
        }

        

    }
}
