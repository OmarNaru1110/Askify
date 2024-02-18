using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;

namespace Askify.Repositories
{
    public class UserAnswerLikesRepository : IUserAnswerLikesRepository
    {
        private readonly ApplicationContext _context;

        public UserAnswerLikesRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Add(UserAnswerLikes userAnswerLikes)
        {
            _context.UserAnswerLikes.Add(userAnswerLikes);
            Save();
        }

        public bool CheckExistance(UserAnswerLikes userAnswerLikes)
        {
            return _context.UserAnswerLikes.Contains(userAnswerLikes);
        }

        public int GetNumberOfLikes(int answerId, int userId)
        {
           return _context.UserAnswerLikes.Where(x=>x.AnswerID==answerId &&  x.UserId==userId).Count();   
        }

        public void Remove(UserAnswerLikes userAnswerLikes)
        {

            _context.UserAnswerLikes.Remove(userAnswerLikes);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges(); 
        }
    }
}
