using Askify.Repositories.IRepositories;
using Askify.Services.IServices;

namespace Askify.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public void Delete(int questionId)
        {
            _questionRepository.Delete(questionId);
        }
    }
}
