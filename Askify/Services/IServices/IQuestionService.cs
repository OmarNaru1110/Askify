using Askify.Models;
using Askify.ViewModels;

namespace Askify.Services.IServices
{
    public interface IQuestionService
    {
        public bool AnswerQuestion(int questionId);
        public Question? CreateQuestionWithSenderIncluded(int? questionId);
        public void Delete(int questionId);
        public bool SendQuestion(string? text, string? Anonymous, int receiverId);
        public List<QuestionVM> GetInbox();
    }
}
