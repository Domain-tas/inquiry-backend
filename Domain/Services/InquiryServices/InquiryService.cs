using Persistence.Models;
using Persistence.Models.Enums;
using Persistence.Repositories.InquiryRepository;

namespace Domain.Services.InquiryServices;
internal class InquiryService : IInquiryService
{
	IInquiryRepository _inquiryRepository;
	public InquiryService(IInquiryRepository inquiryRepository)
	{
		_inquiryRepository = inquiryRepository;
	}

	public async Task<IList<Inquiry>> GetAsync(Guid userId)
	{
		return await _inquiryRepository.GetAsync(x => x.UserId == userId);
	}

    public async Task<Inquiry?> PostAsync(Guid userId, string body, InquiryType type)
    {
		return await _inquiryRepository.InsertAsync(userId, body, type);
    }

	public async Task<Inquiry?> UpdateAsync(Guid inquiryId, InquiryStatus status)
	{
		return await _inquiryRepository.UpdateAsync(inquiryId, status);
	}
}
