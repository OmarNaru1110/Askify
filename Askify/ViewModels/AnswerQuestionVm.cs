using System.ComponentModel.DataAnnotations;

namespace Askify.ViewModels
{
    public class AnswerQuestionVm
    {
        [Required]
        public string AnswerText { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerReceiverId { get; set; }
    }
}
