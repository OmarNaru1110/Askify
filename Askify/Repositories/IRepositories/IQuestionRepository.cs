﻿using Askify.Models;

namespace Askify.Repositories.IRepositories
{
    public interface IQuestionRepository
    {
        public Question? GetQuestion(int questionId);
        public void Delete(int questionId);
        public bool AnswerQuestion(int questionId);
        public void Save();
        public Question? GetById(int questionId);
        public void SendQuestion(Question question);
        public List<Question> GetInbox();
        public Question? CreateQuestionWithSenderIncluded(int? questionId);
        public Question? GetQuestionChildrenWithTheirAnswers(int parentQuestionId);
    }
}
