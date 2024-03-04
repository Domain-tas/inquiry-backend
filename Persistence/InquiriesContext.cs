using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence;

public class InquiriesContext : DbContext
{
	public InquiriesContext(DbContextOptions options) : base(options)
	{
	}
	public DbSet<User> Users { get; set; }
	public DbSet<Inquiry> Inquiries { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			.HasMany(e => e.Inquiries)
			.WithOne(e => e.User)
			.HasForeignKey(e => e.UserId)
			.IsRequired();
	}
}
