using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;
using Askify.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Askify.Repositories
{
    public class EnduserRepository : IEnduserRepository
    {
        private readonly ApplicationContext _context;
        private readonly IAccountService _accountService;

        public EnduserRepository( ApplicationContext context,IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        public void UpdateUserName(int userId, string userName)
        {
            var user = GetById(userId);
            user.UserName= userName;
            Save();
        }
        public bool RemoveFollowing(int anotherUserId)
        {
            int? curUserId = _accountService.GetCurrentEndUserId();
            if(curUserId==null) return false;
            var curUser = _context.EndUsers.Include(x => x.Following).FirstOrDefault(c => c.Id == curUserId);
            if(curUser==null) return false;
            var anotherUser = GetById(anotherUserId);
            if(anotherUser==null) return false;
            curUser.Following.Remove(anotherUser);
            Save();
            return true;
        }
        public bool AddFollowing(int anotherUserId)
        {
            int? curUserId = _accountService.GetCurrentEndUserId();
            if (curUserId == null) return false;
            var curUser = _context.EndUsers.Include(x => x.Following).FirstOrDefault(c => c.Id == curUserId);
            if (curUser == null) return false;
            var anotherUser = GetById(anotherUserId);
            if (anotherUser == null) return false;
            curUser.Following.Add(anotherUser);
            Save();
            return true;
        }
        public List<EndUser> GetFollowersList(int userId)
        {
            return _context.EndUsers.Include(x => x.Followers).FirstOrDefault(x => x.Id == userId).Followers.ToList();

        }
        public List<EndUser> GetFollowingList(int userId)
        {
            return _context.EndUsers.Include(x => x.Following).FirstOrDefault(x=>x.Id==userId).Following.ToList();
        }

        public int GetFollowersCount(int endUserId)
        {
            EndUser? user = _context.EndUsers
                .Include(u => u.Followers)
                .FirstOrDefault(x => x.Id == endUserId);
            if (user != null)
            {
                int followersCount = user.Followers.Count;
                return followersCount;
            }
            return 0;
        }
        public int GetFollowingCount(int endUserId)
        {
            EndUser? user = _context.EndUsers
                 .Include(u => u.Following) 
                 .FirstOrDefault(x => x.Id == endUserId);
            if (user != null)
            {
                int followingCount = user.Following.Count;
                return followingCount;
            }
            return 0;
        }   
        public int GetAnswersCount(int endUserId)
        {
            EndUser? user = _context.EndUsers
                .Include(u => u.SentAnswers)
                .FirstOrDefault(x => x.Id == endUserId);
            if (user != null)
            {
                int answersCount = user.SentAnswers .Count;
                return answersCount;
            }
            return 0;
        }
        public EndUserDetails GetUserDetails(int endUserId)
        {
            return new EndUserDetails {
                FollowersCount = GetFollowersCount(endUserId),
                FollowingCount = GetFollowingCount(endUserId),
                AnswersCount = GetAnswersCount(endUserId)
            };
        }
        public async Task<(AppUser?, IdentityResult)> Add(UserRegisterationVM userVM, UserManager<AppUser> userManager)
        {
            //AppUser returnedUser;
            //IdentityResult returnedResult;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //create enduser
                    var user = new EndUser { UserName = userVM.Username };
                    //add enduser
                    _context.EndUsers.Add(user);
                    Save();
                    //now get it's id to add identity user
                    var id = GetIdByUsername(user.UserName);
                    if (id == null)
                        throw new Exception();
                    var appUser = new AppUser
                    {
                        UserName = userVM.Username,
                        Email = userVM.EmailAddress,
                        PasswordHash = userVM.Password,
                        EndUserId = id.Value
                    };

                    var result = await userManager.CreateAsync(appUser,userVM.Password);
                    if(result.Succeeded == false)
                        throw new Exception();
                    
                    //returnedUser = appUser;
                    //returnedResult = result;
                    
                    transaction.Commit();

                    return (appUser, result);
                } 
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (null,IdentityResult.Failed());
                }
            }
            //return (returnedUser, returnedResult);
        }

        public EndUser? GetById(int id)
        {
            return _context.EndUsers.FirstOrDefault(x => x.Id == id);
        }

        public int? GetIdByUsername(string username)
        {
            return _context.EndUsers.FirstOrDefault(x => x.UserName == username)?.Id;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool CheckIsFollowing(int anotherUserId)
        {
            var myId = _accountService.GetCurrentEndUserId();
            if(myId == null) 
                return false;
            return _context.EndUsers.Include(x => x.Following).FirstOrDefault(x => x.Id == myId).Following.Any(c => c.Id == anotherUserId);
        }
    }
}
