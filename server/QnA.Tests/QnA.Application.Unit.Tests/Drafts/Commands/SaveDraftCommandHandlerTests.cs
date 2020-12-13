using FizzWare.NBuilder;
using Moq;
using QnA.Application.Drafts.Commands;
using QnA.Application.Exceptions;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QnA.Application.Unit.Tests.Drafts.Commands
{
    public class SaveDraftCommandHandlerTests
    {
        private Mock<IQuestionsRepository> _questionsRepository;
        private Mock<IDraftRepository> _draftsRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        public SaveDraftCommandHandlerTests()
        {
            _questionsRepository = new Mock<IQuestionsRepository>();
            _draftsRepository = new Mock<IDraftRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async void Throw_Exception_For_Invalid_Question()
        {

            _questionsRepository
                .Setup(x => x.QuestionExists(It.IsAny<int>()))
                .Returns(Task.FromResult(false));

            var command = new SaveDraftCommand { UserId = 1, Content = "anything", QuestionId = 1 };
            var commandHandler = new SaveDraftCommandHandler(
                _draftsRepository.Object,
                _questionsRepository.Object,
                _unitOfWork.Object);

            try
            {
                var result = await commandHandler.Handle(command, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Assert.True(ex is InvalidQuestionException);
            }

        }


        [Fact]
        public async void Add_New_Draft_When_No_Existing_Draft() {

            var question = Builder<Draft>.CreateNew()
                            .With(x => x.QuestionId = 1)
                            .With(x => x.UserId = 1).Build();

            _questionsRepository.Setup(x => x.QuestionExists(1))
                        .Returns(Task.FromResult(true));

            _draftsRepository.Setup(x => x.GetByQuestionAndUser(1, 1))
                        .Returns(Task.FromResult<Draft>(null));

            var command = new SaveDraftCommand { QuestionId = 1, UserId = 1, Content = "any" };
            var commandHandler = new SaveDraftCommandHandler(
                _draftsRepository.Object, 
                _questionsRepository.Object, 
                _unitOfWork.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);


            Assert.Equal("Draft saved successfully.", result.Message);
            _draftsRepository.Verify(x => x.AddAsync(It.IsAny<Draft>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        }

        [Fact]
        public async void If_Draft_Exists_Then_Update_Existing_Draft()
        {
            var draft = Builder<Draft>.CreateNew()
                            .With(x => x.QuestionId = 1)
                            .With(x => x.UserId = 1).Build();

            _questionsRepository.Setup(x => x.QuestionExists(1))
                        .Returns(Task.FromResult(true));

            _draftsRepository.Setup(x => x.GetByQuestionAndUser(1, 1))
                        .Returns(Task.FromResult(draft));

            var command = new SaveDraftCommand { QuestionId = 1, UserId = 1, Content = "any" };
            var commandHandler = new SaveDraftCommandHandler(
                _draftsRepository.Object,
                _questionsRepository.Object,
                _unitOfWork.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.Equal("Draft saved successfully.", result.Message);                                  
        }
    }
}
