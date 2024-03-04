using Persistence.Models;
using System.Linq.Expressions;

namespace Persistence.Repositories.UserRepository;
public interface IUserRepository
{
	Task<IList<User>> GetAsync();
	Task<User?> GetAsync(Guid userId);
	Task<IList<User>> GetAsync(Expression<Func<User, bool>> predicate);
	Task<User?> InsertAsync(string username, string password, string salt);
}