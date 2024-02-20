using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.RepoConcrete;
using Repositories.RepoContracts;
using System.Runtime.CompilerServices;

namespace WebApi.ExtensionMethods
{
	public static class ServiceExtensions
	{
		public static void SqlConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<RepositoryContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
		}
		public static void RepositoryInjections(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryManager, RepositoryManager>();
			services.AddScoped<IBookRepository, BookRepository>();
		}
	}
}
