using Askify.Models;

namespace Askify.Repositories.IRepositories
{
    public interface ITimelineRepository
    {
        public List<Answer>? GetFollowingAnswers(int endUserId, int page, int size);
        public int GetTimelineAnswersCount(int userId);
    }
}
