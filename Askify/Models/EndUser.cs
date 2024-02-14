namespace Askify.Models
{
    public class EndUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<EndUser> Followers { get; set; } = new List<EndUser>();
        public ICollection<EndUser> Following { get; set; } = new List<EndUser>();

        public ICollection<Question> ReceivedQuestions { get; set; }
        public ICollection<Question> SentQuestions { get; set; }
        public ICollection<Answer> SentAnswers { get; set; }
        public ICollection<Answer> ReceivedAnswers{ get; set; }
        public ICollection<UserAnswerLikes> LikedAnswers { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public AppUser IdentityUser { get; set; }
    }
}
 