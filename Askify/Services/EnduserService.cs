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

        public EnduserService(IEnduserRepository enduserRepository)
        {
            _enduserRepository = enduserRepository;
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
    }
}
