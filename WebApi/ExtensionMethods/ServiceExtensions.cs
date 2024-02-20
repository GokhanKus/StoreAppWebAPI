using Microsoft.EntityFrameworkCore;
using Repositories.Context;
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
	}
}
