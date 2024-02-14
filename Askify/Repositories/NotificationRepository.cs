using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Askify.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationContext _context;

        public NotificationRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public List<Notification>? GetNotifications(int endUserId)
        {
            return _context.Notifications
                .Where(x=>x.ReceiverId == endUserId)
                .Include(x=>x.Answer)
                .ThenInclude(x=>x.Sender)
                .Include(x => x.Answer)
                .ThenInclude(x => x.Question)
                .Include(x=>x.Receiver)
                .OrderByDescending(x=>x.Answer.CreatedDate)
                .ToList();
        }

        public Notification? Get(int notificationId)
        {
            return _context.Notifications.FirstOrDefault(x=>x.Id==notificationId);
        }
    }
}
