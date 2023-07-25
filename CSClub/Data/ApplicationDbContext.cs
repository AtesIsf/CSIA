using CSClub.Data;
using Microsoft.EntityFrameworkCore;

namespace CSClub.Data;

public class ApplicationDbContext : DbContext
{
	public DbSet<ClubMember> Members { get; set; }

	public DbSet<AdminModel> Admins { get; set; }

	public DbSet<TeacherModel> Teachers { get; set; }

	public DbSet<LogModel> Logs { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}
}

