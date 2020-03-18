using Moq;
using QnA.Application.Answers.Commands;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QnA.Application.Unit.Tests.Answers.Commands
{
    public class PublishAnswerCommandHandler
    {
        private Mock<IQuestionsRepository> _questionsRepository;
        private Mock<IAnswersRepository> _answersRepository;
        private Mock<IQuestionsFollowingRepository> _questionsFollowingRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        public PublishAnswerCommandHandler()
        {
            _questionsRepository = new Mock<IQuestionsRepository>();
            _answersRepository = new Mock<IAnswersRepository>();
            _questionsFollowingRepository = new Mock<IQuestionsFollowingRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }
        [Fact]
        public async void Publish_Answer_Success_When_Question_Exists_And_Has_Followers()
        {            
            _questionsRepository.Setup(r => r.QuestionExists(1)).Returns(Task.FromResult(true));
            _answersRepository.Setup(x => x.AddAsync(It.IsAny<Answer>())).Returns(Task.CompletedTask);
            _questionsFollowingRepository.Setup(x => x.QuestionsHasFollowers(1)).Returns(Task.FromResult(true));
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            var command = new PublishAnswerCommand { QuestionId = 1, UserId = 1, Answer = "answer" };
            var commandHandler = new Application.Answers.Commands.PublishAnswerCommandHandler(
                _questionsRepository.Object,
                _answersRepository.Object,
                _questionsFollowingRepository.Object,
                _unitOfWork.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);
            
            Assert.Equal("Answer published successfully", result.Message);
            Assert.True(result.HasFollowers);
                                                
        }
        [Fact]
        public async void Publish_Answer_Success_When_Question_Exists_And_Has_No_Followers()
        {
            
            _questionsRepository.Setup(r => r.QuestionExists(1)).Returns(Task.FromResult(true));
            _answersRepository.Setup(x => x.AddAsync(It.IsAny<Answer>())).Returns(Task.CompletedTask);
            _questionsFollowingRepository.Setup(x => x.QuestionsHasFollowers(1)).Returns(Task.FromResult(false));
            _unitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            var command = new PublishAnswerCommand { QuestionId = 1, UserId = 1, Answer = "answer" };
            var commandHandler = new Application.Answers.Commands.PublishAnswerCommandHandler(
                _questionsRepository.Object,
                _answersRepository.Object,
                _questionsFollowingRepository.Object,
                _unitOfWork.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal("Answer published successfully", result.Message);
            Assert.False(result.HasFollowers);
        }

        [Fact]
        public async void Publish_Answer_Fail_When_Question_Not_Exists()
        {
            
            _questionsRepository.Setup(r => r.QuestionExists(1)).Returns(Task.FromResult(false));           

            var command = new PublishAnswerCommand { QuestionId = 1, UserId = 1, Answer = "answer" };
            var commandHandler = new Application.Answers.Commands.PublishAnswerCommandHandler(
                _questionsRepository.Object,
                _answersRepository.Object,
                _questionsFollowingRepository.Object,
                _unitOfWork.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal("Question does not exist.", result.Message);
            
        }
    }
}
