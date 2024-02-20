using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Entities.Models;
using WebApi.Repositories.Config;

namespace WebApi.Repositories
{
	public class RepositoryContext : DbContext
	{
		public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) //base ifadesi dbcontexti temsil ediyor, dbcontextteki baglanti dizesini kullanacagimizi belirtiyorz.
		{

		}
		public DbSet<Book> Books { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BookConfig()); //BookConfig seeding classını calistirir.
			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //butun seed datalari calistirir
		}
	}
}
