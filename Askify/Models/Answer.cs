namespace Askify.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public EndUser Sender { get; set; }
        public int ReceiverId { get; set; }
        public EndUser Receiver { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public bool IsSeen { get; set; }
        public ICollection<Notification> Notification { get; set; }
        public ICollection<UserAnswerLikes> UsersLikes { get; set; }

    }
}
