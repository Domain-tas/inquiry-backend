using System.Security.Cryptography;
using System.Text;

namespace Domain.Helpers;
internal static class SecurityHelper
{
	internal static (string salt, string password) HashPassword(string password)
	{
		var salt = GetSalt();
		var hashedPassword = GetHash(password, salt);
		return (salt, hashedPassword);
	}

	internal static bool ValidateHash(string salt, string storedPassword, string loginPassword)
	{
		var hashedPassword = GetHash(loginPassword, salt);
		if (hashedPassword == storedPassword)
			return true;
		else
			return false;
	}

	private static string GetHash(string password, string salt)
	{
		using var sha256 = SHA256.Create();
		var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
		return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
	}
	private static string GetSalt()
	{
		var bytes = new byte[128 / 8];
		using var randomGenerator = RandomNumberGenerator.Create();
		randomGenerator.GetBytes(bytes);
		var salt = BitConverter.ToString(bytes).Replace("-", "");
		return salt;
	}
}
