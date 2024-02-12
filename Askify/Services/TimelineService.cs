using Askify.Models;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;

namespace Askify.Services
{
    public class TimelineService: ITimelineService
    {
        private readonly ITimelineRepository _timelineRepository;

        public TimelineService(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }
        public List<Answer>? GetFollowingAnswers(int? endUserId, int page, int size)
        {
            if(endUserId == null)
                return null;
            return _timelineRepository.GetFollowingAnswers(endUserId.Value, page, size);
        }
        public int GetTimelineAnswersCount(int userId)
        {
            return _timelineRepository.GetTimelineAnswersCount(userId);
        }
    }
}
