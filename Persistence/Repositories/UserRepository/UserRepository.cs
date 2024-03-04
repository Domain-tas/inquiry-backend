using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using System.Linq.Expressions;

namespace Persistence.Repositories.UserRepository;
public class UserRepository : IUserRepository
{
	InquiriesContext _context;
	public UserRepository(InquiriesContext context)
	{
		_context = context;
	}

	public async Task<IList<User>> GetAsync()
	{
		var result = await _context.Users.AsNoTracking().ToListAsync();
		return result;
	}

	public async Task<User?> GetAsync(Guid userId)
	{
		var result = await _context.Users.FindAsync(userId);
		return result;
	}

	public async Task<IList<User>> GetAsync(Expression<Func<User, bool>> predicate)
	{
		return await _context.Users.Where(predicate).ToListAsync();
	}

	public async Task<User?> InsertAsync(string username, string password, string salt)
	{
		var user = new User
		{
			Id = Guid.NewGuid(),
			Name = username,
			Password = password,
			Salt = salt
		};
		await _context.AddAsync(user);
		await _context.SaveChangesAsync();
		return await GetAsync(user.Id);
	}
}
