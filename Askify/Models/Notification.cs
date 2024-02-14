namespace Askify.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int ReceiverId { get; set; }
        public EndUser Receiver { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public bool IsSeen { get; set; } = false;

    }
}
