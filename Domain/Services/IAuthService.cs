using Domain.Dtos;
using Persistence.Models;

namespace Domain.Services;
public interface IAuthService
{
	Task<TokenDto?> LoginUserAsync(string username, string loginPassword);
	Task<User?> RegisterUserAsync(string username, string password);
}