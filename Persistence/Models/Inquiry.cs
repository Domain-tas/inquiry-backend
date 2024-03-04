using Persistence.Models.Enums;

namespace Persistence.Models;
public class Inquiry
{
	public Guid Id {  get; set; }
	public DateTime DateSubmitted { get; set; }
	public string? Body { get; set; }
	public InquiryType Type { get; set; }
	public InquiryStatus Status { get; set; }
	public User User { get; set; } = null!;
	public Guid UserId { get; set; }
}
