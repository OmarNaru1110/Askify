using Askify.Models;

namespace Askify.Services.IServices
{
    public interface IAccountService
    {
        Task<AppUser?> GetAppUser();
        int? GetCurrentEndUserId();
    }
}
