using Askify.Models;

namespace Askify.Repositories.IRepositories
{
    public interface IUserAnswerLikesRepository
    {
        public int GetNumberOfLikes(int answerId, int userId);
        public void Add(UserAnswerLikes userAnswerLikes);
        public void Remove(UserAnswerLikes userAnswerLikes);
        public void Save();
        public bool CheckExistance(UserAnswerLikes userAnswerLikes);
    }
}
