using Askify.Models;
using Askify.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Askify.Services.IServices
{
    public interface IEnduserService
    {
        public List<EndUser> GetFollowingList(int userId);
        public List<EndUser> GetFollowersList(int userId);
        public EndUserDetails GetDetails(int? endUserId);
        public EndUser? GetById(int? id);
        public int? GetIdByUsername(string username);
        public Task<(AppUser?, IdentityResult)> Add(UserRegisterationVM user, UserManager<AppUser> userManager);
    }
}
