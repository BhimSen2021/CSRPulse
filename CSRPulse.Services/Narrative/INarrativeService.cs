using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface INarrativeService
    {
        bool ActiveDeActive(int id, bool IsActive);
        bool CommentRequired(int id, bool commentRequire);
        Task<NarrativeQuestion> CreateAsync(NarrativeQuestion question);
        Task<List<NarrativeQuestion>> GetQuestionAsync(NarrativeQuestion question);
        Task<NarrativeQuestion> GetQuestionByIdAsync(int questionId);
        Task<bool> UpdateQuestionAsync(NarrativeQuestion question);
    }
}