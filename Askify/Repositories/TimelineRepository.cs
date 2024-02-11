using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Askify.Repositories
{
    public class TimelineRepository : ITimelineRepository
    {
        private readonly ApplicationContext _context;

        public TimelineRepository(ApplicationContext context)
        {
            _context = context;
        }
        public List<Answer>? GetFollowingAnswers(int endUserId)
        {
            var followingAnswers = _context.EndUsers
                .Where(u => u.Id == endUserId)
                .SelectMany(u => u.Following.SelectMany(f => f.SentAnswers))
                .Include(answer => answer.Question)
                .Include(answer => answer.Sender)
                .Include(answer => answer.Receiver)
                .OrderByDescending(x=>x.CreatedDate)
                .ToList();

            return followingAnswers;
        }
    }
}
