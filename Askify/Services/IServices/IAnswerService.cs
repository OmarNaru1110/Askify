using Askify.Models;
using Askify.ViewModels;

namespace Askify.Services.IServices
{
    public interface IAnswerService
    {
        public List<Answer> SearchAnswers(string? answerText, int? endUserId);
        public List<Answer>? GetUserAnswers(int? endUserId);
        public Answer? Edit(int? answerId);
        public List<Answer>? GetNotifications();
        public bool Add(AnswerQuestionVm obj);
    }
}
