

namespace Persistence.Models;
public class User
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string Salt { get; set; } = null!;
	public IList<Inquiry>? Inquiries { get; set; }
}
