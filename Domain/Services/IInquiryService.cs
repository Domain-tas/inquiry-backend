using Persistence.Models;
using Persistence.Models.Enums;

namespace Domain.Services;
public interface IInquiryService
{
    Task<IList<Inquiry>> GetAsync(Guid userId);
    Task<Inquiry?> PostAsync(Guid userId, string body, InquiryType type);
	Task<Inquiry?> UpdateAsync(Guid inquiryId, InquiryStatus status);
}