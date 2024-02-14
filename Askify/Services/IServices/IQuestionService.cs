using Askify.Models;
using Askify.ViewModels;

namespace Askify.Services.IServices
{
    public interface IQuestionService
    {
        public Question? GetQuestion(int? questionId);
        public bool AnswerQuestion(int questionId);
        public Question? CreateQuestionWithSenderIncluded(int? questionId);
        public List<Question>? GetQuestionChildrenWithTheirAnswers(int? parentQuestionId);
        public void Delete(int questionId);
        public bool SendQuestion(string? text, string? Anonymous, int receiverId,int? parentQuestionId);
        public List<QuestionVM> GetInbox();
    }
}
