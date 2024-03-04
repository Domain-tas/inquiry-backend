using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using Persistence.Models.Enums;
using System.Linq.Expressions;

namespace Persistence.Repositories.InquiryRepository;
internal class InquiryRepository : IInquiryRepository
{
	InquiriesContext _context;

	public InquiryRepository(InquiriesContext context)
	{
		_context = context;
	}
	public async Task<Inquiry?> GetAsync(Guid inquiryId)
	{
		return await _context.Inquiries.FindAsync(inquiryId);
	}

	public async Task<IList<Inquiry>> GetAsync()
	{
		return await _context.Inquiries
			.ToListAsync();
	}

	public async Task<IList<Inquiry>> GetAsync(Expression<Func<Inquiry, bool>> predicate)
	{
		return await _context.Inquiries
			.Where(predicate)
			.ToListAsync();
	}
	public async Task<Inquiry?> InsertAsync(Guid userId, string body, InquiryType type)
	{
		var inquiry = new Inquiry
		{
			Id = Guid.NewGuid(),
			DateSubmitted = DateTime.UtcNow,
			Body = body,
			Type = type,
			Status = InquiryStatus.Submitted,
			UserId = userId
		};

		await _context.AddAsync(inquiry);
		await _context.SaveChangesAsync();

		return await _context.Inquiries.FindAsync(inquiry.Id);
	}

	public async Task<Inquiry?> UpdateAsync(Guid inquiryId, InquiryStatus status)
	{
		var entity = await _context.Inquiries.FindAsync(inquiryId);

		if (entity is null)
			return null;

		entity.Status = status;

		_context.Inquiries.Update(entity);
		await _context.SaveChangesAsync();

		return entity;
	}

}
