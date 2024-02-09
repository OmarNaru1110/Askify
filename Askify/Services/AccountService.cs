using Askify.Models;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;

namespace Askify.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AppUser?> GetAppUser()
        {
            var appUser = await _accountRepository.GetAppUser();
            return appUser;
        }

        public int? GetCurrentEndUserId()
        {
            return _accountRepository.GetCurrentEndUserId().Result;
        }
    }
}
