using Askify.Models;
using Askify.Repositories;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;
using Askify.ViewModels;
using System.Xml;

namespace Askify.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IAccountService _accountService;
        private readonly IQuestionService _questionService;

        public AnswerService(IAnswerRepository answerRepository, 
            IAccountService accountService,
            IQuestionService questionService)
        {
            _answerRepository = answerRepository;
            _accountService = accountService;
            _questionService = questionService;
        }
        public List<Answer>? GetUserAnswers(int? endUserId)
        {
            if (endUserId == null)
                return null;
            return _answerRepository.GetUserAnswers(endUserId.Value);
        }
        public bool Add(AnswerQuestionVm obj)
        {
            if(obj == null || obj.AnswerText == null || obj.QuestionId==null || obj.AnswerReceiverId == null)
                return false;
            int? senderId = _accountService.GetCurrentEndUserId();
            if (senderId == null)
                return false;

            var answer = new Answer
            {
                CreatedDate = DateTime.Now,
                IsSeen = false,
                QuestionId = obj.QuestionId.Value,
                ReceiverId = obj.AnswerReceiverId.Value,
                SenderId = senderId.Value,
                Text = obj.AnswerText
            };

            if (_questionService.AnswerQuestion(obj.QuestionId.Value) == false)
                return false;

            _answerRepository.Add(answer);
            return true;
        }

        public Answer? Edit(int? answerId)
        {
            if (answerId == null)
                return null;
            var answer = _answerRepository.Get(answerId.Value);
            if (answer == null)
                return null;
            answer.IsSeen = true;
            _answerRepository.Save();
            return answer;
        }

        public List<Answer>? GetNotifications()
        {
            int? endUserId = _accountService.GetCurrentEndUserId();
            if (endUserId == null)
                return null;
            return _answerRepository.GetNotifications(endUserId.Value);
        }

        public List<Answer> SearchAnswers(string? answerText, int? endUserId)
        {
            if (answerText == null || endUserId == null)
                return new List<Answer>();
            var searchResult = _answerRepository.SearchAnswers(answerText, endUserId.Value);
            return searchResult == null ? new List<Answer>() : searchResult;
        }
    }
}
