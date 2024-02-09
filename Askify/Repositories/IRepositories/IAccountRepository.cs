using Askify.Models;

namespace Askify.Repositories.IRepositories
{
    public interface IAccountRepository
    {
        Task<AppUser?> GetAppUser();
        Task<int?> GetCurrentEndUserId();
    }
}
