using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Application.Questions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Feed.Queries
{
    public class GetFeedByPageQueryHandler : IRequestHandler<GetFeedByPageQuery, List<QuestionDto>>
    {

        private readonly IQuestionsRepository _questionsRepository;
        private readonly IPlaceHolderImageProvider _placeholderImageProvider;

        public GetFeedByPageQueryHandler(IQuestionsRepository questionsRepository, IPlaceHolderImageProvider placeholderImageProvider)
        {
            _questionsRepository = questionsRepository;
            _placeholderImageProvider = placeholderImageProvider;
        }
        public async Task<List<QuestionDto>> Handle(GetFeedByPageQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionsRepository.GetQuestionsPagedData(request.Page);

            //TODO: replace logic using AutoMapper
            var questionDtos = new List<QuestionDto>();
            foreach (var ques in questions)
                questionDtos.Add(QuestionDto.FromEntity(ques));

            questionDtos.ForEach(que =>
            {
                if (que.User.Image == null)
                    que.User.Image = _placeholderImageProvider.GetProfileImagePlaceHolder();
            });
            return questionDtos;
        }
    }
}
