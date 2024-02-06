namespace Askify.Models
{
    public class EndUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ProfImage { get; set; }
        public string CoverImage { get; set; }
        public ICollection<EndUser> Followers { get; set; } = new List<EndUser>();
        public ICollection<EndUser> Following { get; set; } = new List<EndUser>();

        public ICollection<Question> ReceivedQuestions { get; set; }
        public ICollection<Question> SentQuestions { get; set; }
        public ICollection<Answer> SentAnswers { get; set; }
        public ICollection<Answer> ReceivedAnswers{ get; set; }
        public ICollection<UserAnswerLikes> LikedAnswers { get; set; }
    }
}
 