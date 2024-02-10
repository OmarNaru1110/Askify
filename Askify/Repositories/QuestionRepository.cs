using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;

namespace Askify.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationContext _context;

        public QuestionRepository(ApplicationContext context)
        {
            _context = context;
        }


        public void Delete(int questionId)
        {
            var question = GetById(questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                Save();
            }
        }

        public Question? GetById(int questionId)
        {
            return _context.Questions.FirstOrDefault(x=>x.Id==questionId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
