using Askify.Models;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;

namespace Askify.Services
{
    public class UserAnswerLikesService : IUserAnswerLikesService
    {
        private readonly IUserAnswerLikesRepository _userAnswerLikesRepository;

        public UserAnswerLikesService(IUserAnswerLikesRepository userAnswerLikesRepository)
        {
            _userAnswerLikesRepository = userAnswerLikesRepository;
        }
        public int GetNumberOfLikes(int answerId, int userId)
        {
            return _userAnswerLikesRepository.GetNumberOfLikes(answerId, userId);
        }
        public bool CheckExistance(UserAnswerLikes userAnswerLikes)
        {
            return _userAnswerLikesRepository.CheckExistance(userAnswerLikes);
        }

        public UserAnswerLikes Create(int answerId, int userId)
        {
            return new UserAnswerLikes { AnswerID=answerId, UserId=userId };
        }

        public int Manage(int answerId, int userId)
        {
            var obj = Create(answerId, userId);

            if(CheckExistance(obj) == true) 
            {
                _userAnswerLikesRepository.Remove(obj);
            }
            else
            {
                _userAnswerLikesRepository.Add(obj);
            }
            return GetNumberOfLikes(answerId, userId);
        }
    }
}
