using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using WebApi.ExtensionMethods;

namespace WebApi
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			//Dbcontexte ihtiyacim oldugunda reposcontext ver, di ioc
			//injection 3 kýsma ayrilir once kayit islemi sonra cozme islemi sonrada yasam suresi (Register, Resolve, Dispose)
			
			builder.Services.SqlConfiguration(builder.Configuration);
			builder.Services.RepositoryInjections();
			builder.Services.ServiceInjections();

			builder.Services.AddControllers().AddNewtonsoftJson();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
