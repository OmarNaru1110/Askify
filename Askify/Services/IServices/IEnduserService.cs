using Askify.Models;
using Askify.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Askify.Services.IServices
{
    public interface IEnduserService
    {
        public List<EndUser>? Search(string? username);
        public bool RemoveFollowing(int? anotherUserId);
        public bool AddFollowing(int? anotherUserId);
        public bool CheckIsFollowing(int anotherUserId);
        public List<EndUser> GetFollowingList(int userId);
        public List<EndUser> GetFollowersList(int userId);
        public int GetFollowingCount(int endUserId);
        public int GetFollowersCount(int endUserId);
        public void UpdateUserName(int userId, string userName);
        public EndUserDetails GetDetails(int? endUserId);
        public EndUser? GetById(int? id);
        public int? GetIdByUsername(string username);
        public Task<(AppUser?, IdentityResult)> Add(UserRegisterationVM user, UserManager<AppUser> userManager);
    }
}
