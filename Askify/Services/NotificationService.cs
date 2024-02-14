using Askify.Models;
using Askify.Repositories;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;
using System.Xml;

namespace Askify.Services
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Notification Create(int answerId, int receiverId)
        {
            return new Notification
            {
                AnswerId = answerId,
                ReceiverId = receiverId,
                IsSeen = false
            };
        }
        public void Send(int answerId, int receiverId)
        {
            var notification = Create(answerId, receiverId);

            _notificationRepository.Add(notification);
        }
        public List<Notification> GetNotifications(int endUserId)
        {
            var notifications = _notificationRepository.GetNotifications(endUserId);
            if (notifications == null)
                return new List<Notification>();
            return notifications;
        }

        public void Edit(int? notificationId)
        {
            if (notificationId == null)
                return;
            var notification = _notificationRepository.Get(notificationId.Value);
            if (notification == null)
                return;
            notification.IsSeen = true;
            _notificationRepository.Save();
        }
    }
}
