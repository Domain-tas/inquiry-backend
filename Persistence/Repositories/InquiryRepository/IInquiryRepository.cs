using Persistence.Models.Enums;
using Persistence.Models;
using System.Linq.Expressions;

namespace Persistence.Repositories.InquiryRepository;
public interface IInquiryRepository
{
	Task<IList<Inquiry>> GetAsync();
	Task<IList<Inquiry>> GetAsync(Expression<Func<Inquiry, bool>> predicate);
	Task<Inquiry?> GetAsync(Guid inquiryId);
	Task<Inquiry?> InsertAsync(Guid userId, string body, InquiryType type);
	Task<Inquiry?> UpdateAsync(Guid inquiryId, InquiryStatus status);
}
