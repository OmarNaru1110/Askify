using Askify.Models;
using Askify.Repositories;
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

        public EnduserService(IEnduserRepository enduserRepository,IAccountService accountService)
        {
            _enduserRepository = enduserRepository;
            _accountService = accountService;
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
        public void UpdateUserName(int userId, string userName)
        {
            _enduserRepository.UpdateUserName(userId, userName);
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

        public int GetFollowingCount(int endUserId)
        {
            return _enduserRepository.GetFollowingCount(endUserId);
        }

        public int GetFollowersCount(int endUserId)
        {
            return _enduserRepository.GetFollowersCount(endUserId);
        }

        public List<EndUser>? Search(string? username)
        {
            if (username == null)
                return null;
            return _enduserRepository.Search(username);
        }
    }
}
