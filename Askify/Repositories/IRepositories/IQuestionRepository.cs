using Askify.Models;

namespace Askify.Repositories.IRepositories
{
    public interface IQuestionRepository
    {
        public void Delete(int questionId);
        public void Save();
        public Question? GetById(int questionId);
    }
}
