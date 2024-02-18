using Askify.Models;

namespace Askify.Services.IServices
{
    public interface IUserAnswerLikesService
    {
        public int Manage(int answerId, int userId);
        public UserAnswerLikes Create(int answerId, int userId);
        public bool CheckExistance(UserAnswerLikes userAnswerLikes);
        public int GetNumberOfLikes(int answerId, int userId);
    }
}
