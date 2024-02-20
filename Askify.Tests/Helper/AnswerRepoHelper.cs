using Askify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Askify.Tests.Helper
{
    static class AnswerRepoHelper
    {
        public static List<Answer> LoadAnswers()
        {
            return new List<Answer>
            {
                new Answer
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    IsSeen = true,
                    SenderId = 1,
                    QuestionId = 1,
                    ReceiverId = 2,
                    Text="",
                    Question = new Question
                    {
                        Id = 1,
                        IsAnonymous = true,
                        IsRepliedTo = true,
                        ReceiverId= 3,
                        SenderId= 4,
                        CreatedDate= DateTime.Now,
                        Text="",
                        ParentQuestionId = null,
                        ChildrenQuestions = new List<Question>()
                    },
                    Notification = new List<Notification>(),
                    Sender = new EndUser
                    {
                        Id =1,
                        UserName = "omar",
                    },
                    Receiver = new EndUser
                    {
                        Id=2,
                        UserName = "ahmed"
                    },
                    UsersLikes = new List<UserAnswerLikes>()
                },
                new Answer
                {
                    Id = 2,
                    CreatedDate = DateTime.Now,
                    IsSeen = true,
                    SenderId = 1,
                    QuestionId = 2,
                    ReceiverId = 3,
                    Text="",
                    Question = new Question
                    {
                        Id = 2,
                        IsAnonymous = true,
                        IsRepliedTo = true,
                        ReceiverId= 3,
                        SenderId= 4,
                        CreatedDate= DateTime.Now,
                        Text="",
                        ParentQuestionId = null,
                        ChildrenQuestions = new List<Question>()
                    },
                    Notification = new List<Notification>(),
                    Sender = new EndUser
                    {
                        Id =1,
                        UserName = "omar",
                    },
                    Receiver = new EndUser
                    {
                        Id=3,
                        UserName = "ahmed"
                    },
                    UsersLikes = new List<UserAnswerLikes>()
                },
                new Answer
                {
                    Id = 3,
                    CreatedDate = DateTime.Now,
                    IsSeen = true,
                    SenderId = 1,
                    QuestionId = 3,
                    ReceiverId = 4,
                    Text="",
                    Question = new Question
                    {
                        Id = 3,
                        IsAnonymous = true,
                        IsRepliedTo = true,
                        ReceiverId= 3,
                        SenderId= 4,
                        CreatedDate= DateTime.Now,
                        Text="",
                        ParentQuestionId = null,
                        ChildrenQuestions = new List<Question>()
                    },
                                        Notification = new List<Notification>(),
                    Sender = new EndUser
                    {
                        Id =1,
                        UserName = "omar",
                    },
                    Receiver = new EndUser
                    {
                        Id=4,
                        UserName = "ahmed"
                    },
                    UsersLikes = new List<UserAnswerLikes>()
                },
                new Answer
                {
                    Id = 4,
                    CreatedDate = DateTime.Now,
                    IsSeen = true,
                    SenderId = 2,
                    QuestionId = 4,
                    ReceiverId = 1,
                    Text="",
                    Question = new Question
                    {
                        Id = 4,
                        IsAnonymous = true,
                        IsRepliedTo = true,
                        ReceiverId= 3,
                        SenderId= 4,
                        CreatedDate= DateTime.Now,
                        Text="",
                        ParentQuestionId = null,
                        ChildrenQuestions = new List<Question>()
                    },
                                        Notification = new List<Notification>(),
                    Sender = new EndUser
                    {
                        Id =2,
                        UserName = "omar",
                    },
                    Receiver = new EndUser
                    {
                        Id=1,
                        UserName = "ahmed"
                    },
                    UsersLikes = new List<UserAnswerLikes>()
                },
                new Answer
                {
                    Id = 5,
                    CreatedDate = DateTime.Now,
                    IsSeen = true,
                    SenderId = 3,
                    QuestionId = 5,
                    ReceiverId = 5,
                    Text="",
                    Question = new Question
                    {
                        Id = 5,
                        IsAnonymous = true,
                        IsRepliedTo = true,
                        ReceiverId= 3,
                        SenderId= 4,
                        CreatedDate= DateTime.Now,
                        Text="",
                        ParentQuestionId = null,
                        ChildrenQuestions = new List<Question>()
                    },
                    Notification = new List<Notification>(),
                    Sender = new EndUser
                    {
                        Id =3,
                        UserName = "omar",
                    },
                    Receiver = new EndUser
                    {
                        Id=5,
                        UserName = "ahmed"
                    },
                    UsersLikes = new List<UserAnswerLikes>()
                },
            };
        }
    }
}
