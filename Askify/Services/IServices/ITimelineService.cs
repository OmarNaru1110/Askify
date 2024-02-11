using Askify.Models;

namespace Askify.Services.IServices
{
    public interface ITimelineService
    {
        public List<Answer>? GetFollowingAnswers(int? endUserId);

    }
}
