using Askify.Models;
using Askify.Repositories;
using Askify.Repositories.IRepositories;
using Askify.Services.IServices;
using Askify.ViewModels;
using Humanizer;

namespace Askify.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAccountService _accountService;
        private readonly IEnduserService _enduserService;

        public QuestionService(IQuestionRepository questionRepository, 
            IAccountService accountService,
            IEnduserService enduserService
            )
        {
            _questionRepository = questionRepository;
            _accountService = accountService;
            _enduserService = enduserService;
        }

        public void Delete(int questionId)
        {
            _questionRepository.Delete(questionId);
        }
        public bool SendQuestion(string? text, string? Anonymous, int receiverId, int? parentQuestionId)
        {
            var question = CreateQuestion(text, Anonymous, receiverId,parentQuestionId);
            if (question == null)
                return false;
            _questionRepository.SendQuestion(question);
            return true;
        }
        public Question? CreateQuestion(string? text, string? Anonymous, int receiverId, int? parentQuestionId)
        {
            if (text == null)
                return null;

            if (parentQuestionId == 0)
                parentQuestionId = null;

            bool isAnonymous = false;

            if (Anonymous != null)
                isAnonymous = true;

            int? senderId = _accountService.GetCurrentEndUserId();
            if (senderId == null)
            {
                return null;
            }

            var question = new Question
            {
                Text = text,
                CreatedDate = DateTime.Now,
                SenderId = senderId.Value,
                ReceiverId = receiverId,
                IsAnonymous = isAnonymous,
                IsRepliedTo = false,
                ParentQuestionId=parentQuestionId
            };
            return question;
        }
        public List<QuestionVM> GetInbox()
        {
            var questions = _questionRepository.GetInbox();
            var inbox = new List<QuestionVM>();
            foreach (var question in questions)
            {
                inbox.Add(new QuestionVM
                {
                    User = _enduserService.GetById(question.SenderId),
                    Question = question
                });
            }

            return inbox;
        }

        public Question? CreateQuestionWithSenderIncluded(int? questionId)
        {
            if(questionId == null) 
                return null;
            return _questionRepository.CreateQuestionWithSenderIncluded(questionId);
        }

        public bool AnswerQuestion(int questionId)
        {
            return _questionRepository.AnswerQuestion(questionId);
        }

        public List<Question>? GetQuestionChildrenWithTheirAnswers(int? parentQuestionId)
        {
            if (parentQuestionId == null)
                return null;
            var parent = _questionRepository.GetQuestionChildrenWithTheirAnswers(parentQuestionId.Value);
            if (parent == null)
                return null;

            var questions = new List<Question> { parent };
            if (parent.ChildrenQuestions == null)
                return questions;

            foreach (var question in parent.ChildrenQuestions)
                questions.Add(question);
        
            return questions.OrderBy(x=>x.CreatedDate).ToList();
        }

        public Question? GetQuestion(int? questionId)
        {
            if(questionId==null)
                return null;
            return _questionRepository.GetQuestion(questionId.Value);
        }
    }
}
