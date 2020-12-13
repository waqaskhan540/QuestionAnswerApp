using FizzWare.NBuilder;
using Moq;
using QnA.Application.Exceptions;
using QnA.Application.Answers.Models;
using QnA.Application.Answers.Queries;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QnA.Application.Unit.Tests.Answers.Queries
{
    public class GetAnswersByQuestionId
    {
        [Fact]
        public async void Returns_Answers_When_Question_Exists()
        {
            var answersRepository = new Mock<IAnswersRepository>();
            var questionsRepository = new Mock<IQuestionsRepository>();

            var answers = Builder<Answer>.CreateListOfSize(5).Build().AsEnumerable();
            answersRepository.Setup(x => x.GetAnswersByQuestionId(1))
                        .Returns(Task.FromResult(answers));
            questionsRepository.Setup(x => x.QuestionExists(1)).Returns(Task.FromResult(true));
            
            var queryHandler = new GetAnswersByQuestionIdQueryHandler(answersRepository.Object,questionsRepository.Object);
            var query = new GetAnswersByQuestionIdQuery(questionId: 1);

            var result = await queryHandler.Handle(query, cancellationToken: CancellationToken.None);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async void Throw_Exception_When_Question_Does_Not_Exist()
        {
            var answersRepository = new Mock<IAnswersRepository>();
            var questionsRepository = new Mock<IQuestionsRepository>();

            questionsRepository.Setup(x => x.QuestionExists(1))
                .Returns(Task.FromResult(false));

            var query = new GetAnswersByQuestionIdQuery(questionId: 1);
            var queryHandler = new GetAnswersByQuestionIdQueryHandler(answersRepository.Object,questionsRepository.Object);

            try
            {
                var result = await queryHandler.Handle(query, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Assert.True(ex is InvalidQuestionException);
            }



                
        }
    }
}
