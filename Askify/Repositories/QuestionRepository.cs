﻿using Askify.Models;
using Askify.Repositories.Context;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Askify.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationContext _context;
        private readonly IAccountService _accountService;

        public QuestionRepository(ApplicationContext context,IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
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
        public List<Question> GetInbox()
        {
            int? userId = _accountService.GetCurrentEndUserId();
            if (userId == null)
            {
                return null;
            }
            var questions = _context.EndUsers
                .Include(x => x.ReceivedQuestions)
                .FirstOrDefault(x => x.Id == userId)
                .ReceivedQuestions.Where(x => x.IsRepliedTo == false).ToList();

            return questions;
        }
        public void SendQuestion(Question question)
        {
            _context.EndUsers.Include(x => x.SentQuestions).FirstOrDefault(x => x.Id == question.SenderId).SentQuestions.Add(question);
            Save();
        }

        public Question? CreateQuestionWithSenderIncluded(int? questionId)
        {
            return _context.Questions.Include(x=>x.Sender).FirstOrDefault(x=>x.Id== questionId);
        }

        public bool AnswerQuestion(int questionId)
        {
            var question = GetById(questionId);
            if (question == null)
                return false;
            question.IsRepliedTo =true;
            Save();
            return true;
        }

        public Question? GetQuestionChildrenWithTheirAnswers(int parentQuestionId)
        {
            return _context.Questions
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Answers)
                .Include(x => x.ChildrenQuestions)
                    .ThenInclude(child => child.Answers)
                .Include(x => x.ChildrenQuestions)
                    .ThenInclude(child => child.Sender)
                .Include(x => x.ChildrenQuestions)
                    .ThenInclude(child => child.Receiver)
                .FirstOrDefault(x => x.Id == parentQuestionId);
        }

        public Question? GetQuestion(int questionId)
        {
            return _context.Questions
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Answers)
                .Include(x => x.ChildrenQuestions)
                .FirstOrDefault(x=>x.Id==questionId);
        }
    }
}
