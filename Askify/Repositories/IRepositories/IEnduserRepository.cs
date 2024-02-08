using Askify.Models;
using Askify.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Askify.Repositories.IRepositories
{
    public interface IEnduserRepository
    {
        public List<EndUser> GetFollowingList(int userId);
        public List<EndUser> GetFollowersList(int userId);
        public EndUserDetails GetUserDetails(int endUserId);
        public int GetFollowersCount(int endUserId);
        public int GetFollowingCount(int endUserId);
        public int GetAnswersCount(int endUserId);
        public int? GetIdByUsername(string username);
        public EndUser? GetById(int id);
        public Task<(AppUser?, IdentityResult)> Add(UserRegisterationVM user,UserManager<AppUser> userManager);
        public void Save();
    }
}
