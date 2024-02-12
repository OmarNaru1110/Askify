using Askify.Models;

namespace Askify.Services.IServices
{
    public interface ITimelineService
    {
        public List<Answer>? GetFollowingAnswers(int? endUserId, int page, int size);
        public int GetTimelineAnswersCount(int userId);

    }
}
