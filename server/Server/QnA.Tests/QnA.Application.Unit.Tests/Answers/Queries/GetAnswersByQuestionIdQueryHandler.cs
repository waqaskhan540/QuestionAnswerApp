using FizzWare.NBuilder;
using Moq;
using QnA.Application.Answers.Models;
using QnA.Application.Answers.Queries;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
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
            var answers = Builder<Answer>.CreateListOfSize(5).Build().AsEnumerable();
            answersRepository.Setup(x => x.GetAnswersByQuestionId(1))
                        .Returns(Task.FromResult(answers));

            
            var queryHandler = new GetAnswersByQuestionIdQueryHandler(answersRepository.Object);
            var query = new GetAnswersByQuestionIdQuery(questionId: 1);

            var result = await queryHandler.Handle(query, cancellationToken: CancellationToken.None);
            Assert.Equal(5, result.Count);
        }
    }
}
