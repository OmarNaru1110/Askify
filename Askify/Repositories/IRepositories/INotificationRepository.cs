using Askify.Models;

namespace Askify.Repositories.IRepositories
{
    public interface INotificationRepository
    {
        public Notification? Get(int notificationId);
        public void Add(Notification notification);
        public void Save();
        public List<Notification>? GetNotifications(int endUserId);
    }
}
