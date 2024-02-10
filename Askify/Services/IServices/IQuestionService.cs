using Askify.ViewModels;

namespace Askify.Services.IServices
{
    public interface IQuestionService
    {
        public void Delete(int questionId);
        public bool SendQuestion(string? text, string? Anonymous, int receiverId);
        public List<QuestionVM> GetInbox();
    }
}
