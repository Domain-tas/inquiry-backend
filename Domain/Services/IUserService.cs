using Persistence.Models;

namespace Domain.Services;
public interface IUserService
{
    Task<User?> GetUserAsync(string username);
    Task<User?> InsertUserAsync(string username, string salt, string password);
}