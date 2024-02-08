using Microsoft.AspNetCore.Identity;

namespace Askify.Models
{
    public class AppUser:IdentityUser<int>
    {
        public int EndUserId { get; set; }
        public EndUser EndUser{ get; set; }
    }
}
