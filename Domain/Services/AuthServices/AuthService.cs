using Domain.Dtos;
using Domain.Helpers;
using Microsoft.IdentityModel.Tokens;
using Persistence.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Services.AuthServices;
internal class AuthService : IAuthService
{
    IUserService _userService;
    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User?> RegisterUserAsync(string username, string password)
    {
        var user = await _userService.GetUserAsync(username);

        if (user != null)
            return null;

        (string salt, string password) hashedPassword = SecurityHelper.HashPassword(password);

        return await _userService.InsertUserAsync(username, hashedPassword.salt, hashedPassword.password);
    }

    public async Task<TokenDto?> LoginUserAsync(string username, string loginPassword)
    {
        var user = await _userService.GetUserAsync(username);

        if (user == null)
            return null;

        var isPasswordValid = SecurityHelper.ValidateHash(user.Salt, user.Password, loginPassword);
        if (!isPasswordValid)
            return null;

        var claims = GetAuthorizationJwt(user);
		var token =  GenerateJwtToken(claims);

		return token;
    }

    private IList<Claim> GetAuthorizationJwt(User user)
    {
        var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("userName", user.Name),
            };

        return claims;
    }

    private TokenDto GenerateJwtToken(IEnumerable<Claim> claimsList)
    {
        var tokenLifetimeMinutes = 60;
        var tokenExpiration = DateTime.Now.AddMinutes(tokenLifetimeMinutes);
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentFetcher.IssuerSecretKey!));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: "http://localhost:5108/",
            audience: "http://localhost:5108/",
            claims: claimsList,
            expires: tokenExpiration,
            signingCredentials: signinCredentials
            );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        var result = new TokenDto
        {
            Token = tokenString,
            Expires = tokenLifetimeMinutes
		};
        return result;
    }
}

