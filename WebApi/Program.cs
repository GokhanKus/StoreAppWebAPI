using Microsoft.AspNetCore.Mvc;
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

			LogManager.Setup().LoadConfigurationFromFile(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config")); //nlogu baslatip nlog.config dosyasindaki yapılandirmayi yukler

			builder.Services.SqlConfiguration(builder.Configuration);
			builder.Services.RepositoryInjections();
			builder.Services.ServiceInjections();
			builder.Services.LoggerServiceInjections();

			builder.Services.ActionFilterInjections();

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

			builder.Services.Configure<ApiBehaviorOptions>(options => //[ApiController] attribute ile beraberinde gelir (ApiBehaviorOptions)
			{
				options.SuppressModelStateInvalidFilter = true; //modelstate invalid olursa bad request donecegini soyleyelim
			});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			var logger = app.Services.GetRequiredService<ILoggerService>(); //uygulamayi elde ettigimiz asama (var app = builder.Build())'dan sonra ihtiyac duyulan servis alınabilir.
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
