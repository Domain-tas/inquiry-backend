using Persistence.Models;
using Persistence.Repositories.UserRepository;

namespace Domain.Services.UserServices;
internal class UserService : IUserService
{
    IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserAsync(string username)
    {
        var users = await _userRepository.GetAsync(x => x.Name == username);
        return users.FirstOrDefault();
    }

    public async Task<User?> InsertUserAsync(string username, string salt, string password)
    {
        return await _userRepository.InsertAsync(username, password, salt);
    }

}
