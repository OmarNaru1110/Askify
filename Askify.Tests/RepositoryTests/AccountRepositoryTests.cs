using Askify.Models;
using Askify.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

namespace Askify.Tests.RepositoryTests
{
    [TestFixture]
    public class AccountRepositoryTests
    {
        Mock<UserManager<AppUser>> _userManager;

        [SetUp]
        public void Setup()
        {
            Mock<IUserStore<AppUser>> _store = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<UserManager<AppUser>>(_store.Object, null, null, null, null, null, null, null, null);
            _userManager.Object.UserValidators.Add(new UserValidator<AppUser>());
            _userManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());
        }

        [Test]
        public async Task GetAppUser_returnsSameName()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,"omar")
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            httpContextAccessor.Setup(x=>x.HttpContext.User)
                .Returns(principal);
            _userManager.Setup(x => x.GetUserAsync(principal))
                .ReturnsAsync(new AppUser { UserName=principal.Identity.Name });


            var accountRepository = new AccountRepository(httpContextAccessor.Object,_userManager.Object);

            var user = await accountRepository.GetAppUser();
            var expectedUser = new AppUser { UserName = principal.Identity.Name };
            Assert.That(user.UserName, Is.EqualTo(expectedUser.UserName));
        }
        [Test]
        public async Task GetCurrentEndUserId_returnsSameId()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new Claim[]
            {
                new Claim("EndUserId", "123")
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            httpContextAccessor.Setup(x => x.HttpContext.User)
                .Returns(principal);
            _userManager.Setup(x => x.GetUserAsync(principal))
                .ReturnsAsync(new AppUser { EndUserId = 123 });

            var accountRepository = new AccountRepository(httpContextAccessor.Object, _userManager.Object);

            var userId = await accountRepository.GetCurrentEndUserId();

            Assert.That(userId, Is.EqualTo(123));
        }

    }
}
