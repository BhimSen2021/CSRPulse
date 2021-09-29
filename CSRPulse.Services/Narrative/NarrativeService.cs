using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public class NarrativeService : BaseService, INarrativeService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public NarrativeService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<NarrativeQuestion> CreateAsync(NarrativeQuestion question)
        {
            try
            {
                var model = _mapper.Map<DTOModel.NarrativeQuestion>(question);

                var IsExist = _genericRepository.Exists<DTOModel.NarrativeQuestion>(x => x.Question.ToLower() == question.Question.ToLower() && x.ProcessId == question.ProcessId);
                if (!IsExist)
                {
                    question.QuestionId = await _genericRepository.InsertAsync(model);
                }
                question.IsExist = IsExist;
                return question;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<NarrativeQuestion> GetQuestionByIdAsync(int questionId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.NarrativeQuestion>(questionId);
                if (result != null)
                    return _mapper.Map<NarrativeQuestion>(result);
                else
                    return new NarrativeQuestion();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NarrativeQuestion>> GetQuestionAsync(NarrativeQuestion question)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.NarrativeQuestion>(x => (question.IsActiveFilter.HasValue ? x.IsActive == question.IsActiveFilter.Value : 1 > 0) && x.ProcessId == question.ProcessId);
                return _mapper.Map<List<NarrativeQuestion>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateQuestionAsync(NarrativeQuestion question)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.NarrativeQuestion>(x => x.Question.ToLower() == question.Question.ToLower() && x.ProcessId == question.ProcessId && x.QuestionId != question.QuestionId);

                question.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.NarrativeQuestion>(question.QuestionId);
                    if (result != null)
                    {
                        result.Question = question.Question;
                        result.CommentRequire = question.CommentRequire;
                        result.QuestionType = question.QuestionType;
                        result.ContentLimit = question.ContentLimit;
                        result.OrderNo = question.OrderNo;
                        result.IsActive = question.IsActive;
                        result.IsActive = question.IsActive;
                        result.UpdatedOn = question.UpdatedOn;
                        result.UpdatedBy = question.UpdatedBy;
                        result.UpdatedRid = question.UpdatedRid;
                        result.UpdatedRname = question.UpdatedRname;
                        _genericRepository.Update(result);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.NarrativeQuestion>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CommentRequired(int id, bool commentRequire)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.NarrativeQuestion>(id);
                model.CommentRequire = commentRequire;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}

