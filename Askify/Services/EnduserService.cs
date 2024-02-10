using Askify.Models;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;
using Askify.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Askify.Services
{
    public class EnduserService:IEnduserService
    {
        private readonly IEnduserRepository _enduserRepository;
        private readonly IAccountService _accountService;

        public EnduserService(
            IEnduserRepository enduserRepository,
            IAccountService accountService
            )
        {
            _enduserRepository = enduserRepository;
            _accountService = accountService;
        }
        public bool SendQuestion(string? text, string? Anonymous, int receiverId)
        {
            var question = CreateQuestion(text, Anonymous, receiverId);
            if (question == null)
                return false;
            _enduserRepository.SendQuestion(question);
            return true;
        }
        public Question? CreateQuestion(string? text, string? Anonymous,int receiverId)
        {
            if(text== null) 
                return null;

            bool isAnonymous = false;

            if (Anonymous != null)
                isAnonymous = true;

            int? senderId = _accountService.GetCurrentEndUserId();
            if (senderId == null)
            {
                return null;
            }

            var question = new Question
            {
                Text = text,
                CreatedDate = DateTime.Now,
                SenderId = senderId.Value,
                ReceiverId = receiverId,
                IsAnonymous = isAnonymous,
                IsRepliedTo = false
            };
            return question;
        }
        public List<QuestionVM> GetInbox()
        {
            var questions = _enduserRepository.GetInbox();
            var inbox = new List<QuestionVM>();
            foreach (var question in questions)
            {
                inbox.Add(new QuestionVM
                {
                    User = _enduserRepository.GetById(question.SenderId),
                    Question = question
                });
            }

            return inbox;
        }
        public bool CheckIsFollowing(int anotherUserId)
        {
            return _enduserRepository.CheckIsFollowing(anotherUserId);
        }
        public List<EndUser> GetFollowersList(int userId)
        {
            return _enduserRepository.GetFollowersList(userId);
        }
        public List<EndUser> GetFollowingList(int userId)
        {
            return _enduserRepository.GetFollowingList(userId);
        }
        public async Task<(AppUser?, IdentityResult)> Add(UserRegisterationVM user, UserManager<AppUser> userManager)
        {
            return await _enduserRepository.Add(user, userManager);
        }

        public EndUser? GetById(int? id)
        {
            if(id == null) 
                return null; 
            return _enduserRepository.GetById(id.Value);
        }

        public int? GetIdByUsername(string username)
        {
            return _enduserRepository.GetIdByUsername(username);
        }

        public EndUserDetails GetDetails(int? endUserId)
        {
            if (endUserId == null)
                return null;
            return _enduserRepository.GetUserDetails(endUserId.Value);
        }

        public bool RemoveFollowing(int? anotherUserId)
        {
            if (anotherUserId == null)
                return false;
            
            return _enduserRepository.RemoveFollowing(anotherUserId.Value);
        }
        public bool AddFollowing(int? anotherUserId)
        {
            if (anotherUserId == null)
                return false;

            return _enduserRepository.AddFollowing(anotherUserId.Value);
        }


    }
}
