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
        private readonly IEnduserRepository _enduserRepository;
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;

        public AnswerService(IAnswerRepository answerRepository, 
            IAccountService accountService,
            IQuestionService questionService,
            IEnduserRepository enduserRepository,
            INotificationService notificationService)
        {
            _answerRepository = answerRepository;
            _accountService = accountService;
            _questionService = questionService;
            _enduserRepository = enduserRepository;
            _notificationService = notificationService;
        }
        public List<Answer>? GetUserAnswers(int? endUserId, int page, int size)
        {
            if (endUserId == null)
                return null;
            return _answerRepository.GetUserAnswers(endUserId.Value, page, size);
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
            _notificationService.Send(answer.Id, answer.ReceiverId);
            
            //send notification to the parent as well
            var question = _questionService.GetQuestion(obj.QuestionId);
            if(question!=null && question.ParentQuestionId != null)
            {
                var parentQuestion = _questionService.GetQuestion(question.ParentQuestionId);
                if (parentQuestion != null)
                {
                    _notificationService.Send(answer.Id, parentQuestion.SenderId);
                }
            }
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

        public List<Answer> SearchAnswers(string? answerText, int? endUserId)
        {
            if (answerText == null || endUserId == null)
                return new List<Answer>();
            var searchResult = _answerRepository.SearchAnswers(answerText, endUserId.Value);
            return searchResult == null ? new List<Answer>() : searchResult;
        }

        public int GetUserAnswersCount(int? endUserId)
        {
            return endUserId==null? 0 : _enduserRepository.GetAnswersCount(endUserId.Value);
        }
    }
}
