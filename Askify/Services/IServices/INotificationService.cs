using Askify.Models;

namespace Askify.Services.IServices
{
    public interface INotificationService
    {
        public void Send(int answerId, int receiverId);
        public Notification Create(int answerId, int receiverId);
        public List<Notification> GetNotifications(int endUserId);
        public void Edit(int? notificationId);
    }
}
