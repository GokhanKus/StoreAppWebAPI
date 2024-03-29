using Asp.Versioning.ApiExplorer;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

			builder.Services.AddControllers(config =>
			{
				config.RespectBrowserAcceptHeader = true; //artik apimizin icerik pazarligina acik oldugunu ve
				config.ReturnHttpNotAcceptable = true;    //kabul edilmeyen format oldugunda 406 koduyla geri donecegiz
				config.CacheProfiles.Add("1min", new CacheProfile { Duration = 60 });//controllerların basina [ResponseCache(CacheProfileName = "5mins")] yazarak kullanilabilir
			})
			.AddXmlDataContractSerializerFormatters() //xml dosya formatini kabul edecegimizi ve bu formatta output verilebilecegini belirtiyoruz(expandoObject'ten sonra format bozuldu)
			.AddCustomCsvFormatter()                    //kendi yazmis oldugumuz custom csvformatter (expandoObject'ten sonra bu format calismiyor(cunku metottaki <bookDto> tipinde))
			.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
			.AddNewtonsoftJson(opt=>opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

			builder.Services.Configure<ApiBehaviorOptions>(options => //[ApiController] attribute ile beraberinde gelir (ApiBehaviorOptions)
			{
				options.SuppressModelStateInvalidFilter = true; //modelstate invalid olursa bad request donecegini soyleyelim
			});


			builder.Services.SqlConfiguration(builder.Configuration);
			builder.Services.RepositoryInjections();
			builder.Services.ServiceInjections();
			builder.Services.LoggerServiceInjections();

			builder.Services.ActionFilterInjections();
			builder.Services.ConfigureCors();
			builder.Services.DataShaperInjections();
			builder.Services.AddCustomMediaTypes();
			builder.Services.BookLinkInjections();
			builder.Services.ConfigureVersioning();
			builder.Services.ConfigureResponseCaching();
			builder.Services.ConfigureHttpCacheHeaders();

			builder.Services.AddMemoryCache();
			builder.Services.ConfigureRateLimiting();
			builder.Services.AddHttpContextAccessor();

			builder.Services.ConfigureIdentityDbContext();
			builder.Services.ConfigureJWT(builder.Configuration);//(Add.Authentication()) bunun icinde username password middleware active

			builder.Services.AddAutoMapper(typeof(Program));//WebApi

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.ConfigureSwagger();

			var app = builder.Build();

			var logger = app.Services.GetRequiredService<ILoggerService>(); //uygulamayi elde ettigimiz asama (var app = builder.Build())'dan sonra ihtiyac duyulan servis alınabilir.
			app.ConfigureExceptionHandler(logger);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(s =>
				{
					s.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreApp V1");
					s.SwaggerEndpoint("/swagger/v2/swagger.json", "StoreApp V2");
					//eger cok sayida versiyon varsa foreach ile donebiliriz tek tek yazmak mantıklı olmaz
					// Tüm API versiyonları için Swagger belgelerinin endpointlerini otomatik olarak oluşturur
					//var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
					//foreach (var description in provider.ApiVersionDescriptions)
				});
			}
			if (app.Environment.IsProduction())
			{
				app.UseHsts();
			}

			app.UseIpRateLimiting();
			app.UseCors("CorsPolicy");
			app.UseResponseCaching();//cacheleme Corsdan sonra kullanilir cagrilir
			app.UseHttpCacheHeaders();

			app.UseAuthentication(); //once dogrulama islemi
			app.UseAuthorization(); //sonra yetkilendirme islemi yapilir

			app.MapControllers();

			app.Run();
		}
	}
}
