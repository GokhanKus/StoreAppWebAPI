using Microsoft.EntityFrameworkCore;
using NLog;
using Repositories.Context;
using Services.Contracts;
using WebApi.ExtensionMethods;

namespace WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			LogManager.Setup().LoadConfigurationFromFile(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config")); //nlogu baslatip nlog.config dosyasindaki yapýlandirmayi yukler

			builder.Services.SqlConfiguration(builder.Configuration);
			builder.Services.RepositoryInjections();
			builder.Services.ServiceInjections();
			builder.Services.LoggerService();

			builder.Services.AddAutoMapper(typeof(Program));//WebApi

			builder.Services.AddControllers(config =>
			{
				config.RespectBrowserAcceptHeader = true; //artik apimizin icerik pazarligina acik oldugunu ve
				config.ReturnHttpNotAcceptable = true;    //kabul edilmeyen format oldugunda 406 koduyla geri donecegiz

			})
			.AddCustomCsvFormatter()					//kendi yazmis oldugumuz custom csvformatter
			.AddXmlDataContractSerializerFormatters() //xml dosya formatini kabul edecegimizi ve bu formatta output verilebilecegini belirtiyoruz
			.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
			.AddNewtonsoftJson();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			var logger = app.Services.GetRequiredService<ILoggerService>(); //uygulamayi elde ettigimiz asama (var app = builder.Build())'dan sonra ihtiyac duyulan servis alýnabilir.
			app.ConfigureExceptionHandler(logger);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			if (app.Environment.IsProduction())
			{
				app.UseHsts();
			}

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
