using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using QnA.Drafts.Api.Data;
using QnA.Drafts.Api.Helpers;
using QnA.Drafts.Api.Http;
using QnA.Drafts.Api.Models;
using QnA.Drafts.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Domain
{
    public class DraftsService : IDraftService
    {
        
        private readonly IValidator<CreateDraftModel> _createDraftModelValidator;
        private readonly IValidator<UpdateDraftModel> _updateDraftModelValidator;
        
        private readonly IMapper _mapper;
        private readonly IDraftRepository _draftRepository;

        private readonly ICurrentUser _currentUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DraftsService(
            IDraftRepository draftRepository, 
            IValidator<CreateDraftModel> createDraftModelValidator,
            IValidator<UpdateDraftModel> updateDraftModelValidator,
            IMapper mapper,
            ICurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor)
        {
            _draftRepository = draftRepository;
            _createDraftModelValidator = createDraftModelValidator;
            _updateDraftModelValidator = updateDraftModelValidator;
            _mapper = mapper;
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ICustomHttpResponse> Create(CreateDraftModel model)
        {
            var validation = _createDraftModelValidator.Validate(model);
            if (!validation.IsValid)
            {
                string[] errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
                return CustomHttpResponse.Create(HttpStatusCode.BadRequest, errors: errors);                
            }

            Draft draftEntity = _mapper.Map<Draft>(model);
            draftEntity.AuthorId = _currentUser.GetUserId();

            Draft createdDraft = await _draftRepository.Create(draftEntity);
            string resourcePath = $"/{createdDraft.Id}";

            return CustomHttpResponse.Create(HttpStatusCode.Created,errors:null,path: resourcePath);

        }

        public async Task<ICustomHttpResponse> Delete(Guid draftId)
        {
            var draft = await _draftRepository.GetById(draftId);
            if (draft == null)
            {
                string[] errors = new string[] { "draft does not exist" };
                return CustomHttpResponse.Create(HttpStatusCode.BadRequest, errors: errors);
            }

            await _draftRepository.Remove(draft);
            return CustomHttpResponse.Create(HttpStatusCode.OK);
        }

        public async Task<ICustomHttpResponse<IEnumerable<DraftDto>>> GetByAuthorId(string authorId)
        {
            var drafts = await _draftRepository.GetByAuthorId(authorId);
            if (!drafts.Any())
            {
                return new CustomHttpResponse<IEnumerable<DraftDto>>(
                    statusCode: HttpStatusCode.OK,                 
                    errors: null,
                    data: Enumerable.Empty<DraftDto>());
            }
            
            return new CustomHttpResponse<IEnumerable<DraftDto>>(
                statusCode: HttpStatusCode.OK,             
                errors: null,
                data : _mapper.Map<IEnumerable<DraftDto>>(drafts));

        }

        public async Task<ICustomHttpResponse<DraftDto>> GetById(Guid draftId)
        {
            var draft = await _draftRepository.GetById(draftId);
            if (draft == null)
            {
                return new CustomHttpResponse<DraftDto>(
                    statusCode: HttpStatusCode.NotFound,                    
                    errors: null,
                    data: null);
            }
            
            return new CustomHttpResponse<DraftDto>(
                    statusCode: HttpStatusCode.OK,                   
                    errors: null,
                    data: _mapper.Map<DraftDto>(draft));
        }
        
        public async Task<ICustomHttpResponse> Update(UpdateDraftModel model)
        {
            var validation = _updateDraftModelValidator.Validate(model);
            if (!validation.IsValid)
            {               
                return CustomHttpResponse.Create(
                    statusCode: HttpStatusCode.BadRequest,
                    errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());
            }

            var draft = await _draftRepository.GetById(model.DraftId);
            if (draft == null)
            {
                return CustomHttpResponse.Create(
                    statusCode: HttpStatusCode.NotFound,
                    errors: new string[] { "draft doesn't exist" });
            }
             
            draft.Content = model.Content;
            draft.LastModified = DateTime.UtcNow;

            await _draftRepository.Update(draft);
            return CustomHttpResponse.Create(
                    statusCode: HttpStatusCode.OK,
                    errors: null);

        }
    }
}
