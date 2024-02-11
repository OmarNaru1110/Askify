using Askify.Models;

namespace Askify.Repositories.IRepositories
{
    public interface IAnswerRepository
    {
        public List<Answer>? SearchAnswers(string answerText, int endUserId);
        public List<Answer>? GetUserAnswers(int endUserId);
        public List<Answer> GetNotifications(int endUserId);
        public void Add(Answer answer);
        public Answer? Get(int answerId);
        public void Save();
    }
}
