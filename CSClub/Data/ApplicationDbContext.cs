using CSClub.Data;
using Microsoft.EntityFrameworkCore;

namespace CSClub.Data;

public class ApplicationDbContext : DbContext
{
	public DbSet<ClubMember> Members { get; set; }

	public DbSet<AdminModel> Admins { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}
}

