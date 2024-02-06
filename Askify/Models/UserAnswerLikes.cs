namespace Askify.Models
{
    public class UserAnswerLikes
    {
        public int UserId { get; set; }
        public EndUser User { get; set; }
        public int AnswerID { get; set; }
        public Answer Answer { get; set; }
    }
}
