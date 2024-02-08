using Askify.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Askify.Repositories.IRepositories
{
    public class AccountRepository
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

    }
}
