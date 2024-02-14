using Askify.Models;
using Askify.ViewModels;

namespace Askify.Services.IServices
{
    public interface IAnswerService
    {
        public List<Answer> SearchAnswers(string? answerText, int? endUserId);
        public List<Answer>? GetUserAnswers(int? endUserId, int page, int size);
        public int GetUserAnswersCount(int? endUserId);
        public Answer? Edit(int? answerId);
        public bool Add(AnswerQuestionVm obj);
    }
}
