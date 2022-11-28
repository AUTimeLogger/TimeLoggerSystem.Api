using AUTimeManagement.Api.DataAccess.Layer.Model.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AUTimeManagement.Api.DataAccess.Layer.Context;

internal class DataContext : DbContext
{
	private readonly IOptions<DataAccessOptions> _options;

	public DbSet<Department> Departments { get; set; }
	public DbSet<Project> Projects { get; set; }
	public DbSet<ProjectToUser> ProjectAssociation { get; set; }
	public DbSet<WorkUnit> WorkUnits { get; set; }


	public DataContext(IOptions<DataAccessOptions> options) : base()
	{
		_options = options;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var dbType = _options.Value.Database;
		var connStr = _options.Value.ConnectionString;

		switch (dbType)
		{
			case DataAccessOptions.DatabaseType.SQL:
				optionsBuilder.UseSqlServer(connStr, o =>
					o.MigrationsAssembly("Management.Api"));
				break;
			case DataAccessOptions.DatabaseType.Postgres:
				optionsBuilder.UseNpgsql(connStr, o =>
					o.MigrationsAssembly("Management.Api"));
				break;
			case DataAccessOptions.DatabaseType.InMemory:
				optionsBuilder.UseInMemoryDatabase(connStr!);
				break;
			default: throw new InvalidOperationException();
		}
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<ProjectToUser>().ToTable("projectToUser");
		builder.Entity<Project>().ToTable("projects");
		builder.Entity<WorkUnit>().ToTable("works");
		builder.Entity<Department>().ToTable("departments");

		builder.Entity<ProjectToUser>()
			.HasKey(c => new { c.UserId, c.ProjectId });
		builder.Entity<ProjectToUser>().Property(b => b.Created).ValueGeneratedOnAdd();
		builder.Entity<ProjectToUser>().Property(b => b.Updated).ValueGeneratedOnAddOrUpdate();
	}
}
