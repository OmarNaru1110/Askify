using System.Reflection.Metadata.Ecma335;

namespace Askify.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsRepliedTo { get; set; } = false;
        public EndUser Sender { get; set; }
        public EndUser Receiver { get; set; }
        public ICollection<Answer> Answers {  get; set; } 

    }
}
