using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Entities.Models;
using Repositories.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Repositories.Context
{
	public class RepositoryContext : IdentityDbContext<User>
	{
		public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) //base ifadesi dbcontexti temsil ediyor, dbcontextteki baglanti dizesini kullanacagimizi belirtiyorz.
		{

		}
		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/*
			IdentityDbContext'ten kalitim aldiktan sonra migration alirken;
			"Unable to create a 'DbContext' of type ''.The exception 'The entity type 'IdentityUserLogin<string>' requires a primary key to be defined.
			If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'." 
			hatasi aliyorduk, IdentityDbContext'in metoduna erismek ve identity ile ilgili tablolarin otomatik gelmesi icin 
            base.OnModelCreating(modelBuilder); yaziyoruz.
			*/
			base.OnModelCreating(modelBuilder);
			//BookConfig seeding classını calistirir.
			//modelBuilder.ApplyConfiguration(new BookConfig()); 
			//modelBuilder.ApplyConfiguration(new RoleConfig());
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //butun seed datalari calistirir
		}
	}
}
