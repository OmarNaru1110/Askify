using Askify.Models;
using Askify.Repositories;
using Askify.Repositories.Context;
using Askify.Services;
using Askify.Services.IServices;
using Askify.Tests.Helper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Askify.Tests.RepositoryTests
{
    [TestFixture]
    internal class AnswerRepositoryTests
    {
        [Test]
        public void GetUserAnswers_ReturnsEmptyList()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "answers_context").Options;

            var accountService = new Mock<IAccountService>();

            using(var context = new ApplicationContext(options))
            {
                var answerRepo = new AnswerRepository(context,accountService.Object);

                var actualList = answerRepo.GetUserAnswers(1, 1, 1);
                
                CollectionAssert.IsEmpty(actualList);
            }
        }
        [Test]
        public void GetUserAnswers_NumberOfItemsReturnedEqualToSize()
        {
            //////////////
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "answers_context1").Options;

            var accountService = new Mock<IAccountService>();

            using (var context = new ApplicationContext(options))
            {
                var answerRepo = new AnswerRepository(context, accountService.Object);
                foreach (var answer in AnswerRepoHelper.LoadAnswers())
                {
                    answerRepo.Add(answer);
                }

                var actualList = answerRepo.GetUserAnswers(1,1,3);

                Assert.That(actualList.Count,Is.EqualTo(3));
            }
        }
    }
}
