using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Askify.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationContext _context;
        private readonly IAccountService _accountService;

        public AnswerRepository(ApplicationContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public List<Answer>? GetUserAnswers(int endUserId, int page, int size)
        {
            return _context.Answers
                  .Where(x => x.SenderId == endUserId)
                  .Include(x => x.Sender)
                  .Include(x => x.Receiver)
                  .Include(x => x.Question)
                  .OrderByDescending(x => x.CreatedDate)
                  .Skip((page-1) * size).Take(size)
                  .ToList();
        }
        public void Add(Answer answer)
        {
            _context.Answers.Add(answer);
            Save();
        }

        public Answer? Get(int answerId)
        {
            return _context.Answers.Include(x=>x.Question)
                .Include(x=>x.Sender)
                .Include(x=>x.Receiver)
                .FirstOrDefault(x=>x.Id==answerId);
        }

        public List<Answer> GetNotifications(int endUserId)
        {
           return _context.Answers.Include(x => x.Question)
                .Include(x=>x.Sender)
                .Where(x => x.ReceiverId == endUserId)
                .OrderByDescending(x => x.CreatedDate).ToList();   
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Answer>? SearchAnswers(string answerText, int endUserId)
        {
            return _context.Answers
                .Where(x => x.SenderId == endUserId)
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Question)
                .Where(x=>x.Text.Contains(answerText))
                .OrderByDescending(x=>x.CreatedDate)
                .ToList();
        }
    }
}
