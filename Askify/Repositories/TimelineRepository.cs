using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Drawing;

namespace Askify.Repositories
{
    public class TimelineRepository : ITimelineRepository
    {
        private readonly ApplicationContext _context;

        public TimelineRepository(ApplicationContext context)
        {
            _context = context;
        }
        public List<Answer>? GetFollowingAnswers(int endUserId, int page, int size)
        {
            var followingAnswers = _context.EndUsers
                .Where(u => u.Id == endUserId)
                .SelectMany(u => u.Following.SelectMany(f => f.SentAnswers))
                .Include(answer => answer.Question)
                .Include(answer => answer.Sender)
                .Include(answer => answer.Receiver)
                .OrderByDescending(x=>x.CreatedDate)
                .Skip((page - 1) * size).Take(size)
                .ToList();

            return followingAnswers;
        }
        public int GetTimelineAnswersCount(int userId)
        {
            return _context.EndUsers
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Following.SelectMany(f => f.SentAnswers))
                .Count();
        }
    }
}
