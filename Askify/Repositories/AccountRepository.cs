using Askify.Models;
using Askify.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Askify.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public AccountRepository(
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<AppUser?> GetAppUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user;
        }

        public async Task<int?> GetCurrentEndUserId()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            
            return user == null ? null : user.EndUserId;
        }
    }
}
