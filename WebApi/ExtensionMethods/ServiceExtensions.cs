using Microsoft.EntityFrameworkCore;
using NLog.Common;
using Repositories.Context;
using Repositories.RepoConcrete;
using Repositories.RepoContracts;
using Services.Concrete;
using Services.Contracts;
using System.Runtime.CompilerServices;
using Presentation.ActionFilters;
using Entities.DTOs;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace WebApi.ExtensionMethods
{
	public static class ServiceExtensions
	{
		public static void SqlConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			//Dbcontexte ihtiyacim oldugunda reposcontext ver, di ioc
			//injection 3 kısma ayrilir once kayit islemi sonra cozme islemi sonrada yasam suresi (Register, Resolve, Dispose)
			services.AddDbContext<RepositoryContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
		}
		public static void RepositoryInjections(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryManager, RepositoryManager>();
			services.AddScoped<IBookRepository, BookRepository>();
		}
		public static void ServiceInjections(this IServiceCollection services)
		{
			services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped<IBookService, BookService>();
		}
		public static void LoggerServiceInjections(this IServiceCollection services)
		{
			services.AddSingleton<ILoggerService, LoggerService>();
		}
		public static void ActionFilterInjections(this IServiceCollection services)
		{
			services.AddScoped<ValidationFilterAttribute>();
			services.AddSingleton<LogFilterAttribute>(); //loglama islemi icin sadece bir tane nesnenin olusmasi yeterli o yuzden singleton
		}
		public static void ConfigureCors(this IServiceCollection services)
		{
			#region Cors
			/*
			X-Pagination adında bir response.header ifademiz var ve bu ifadenin clientlar tarafından tuketilebilmesi/okunabilmesi icin bir izin tanimlamasi yapmamiz gerek
			bunu da Cors (Cross origin resource sharing) adı verilen yapi icerisinde yapariz
			ornegin front-end uygulama gelistiren birisi bizim api'mize baglanmak istesin
			ve biz bir policy ekleyerek o kisiye bu kaynaga erisme izni vermeliyiz ki api'mize istek atabilsin
			*/
			#endregion
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
				{
					builder
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithExposedHeaders("X-Pagination");
				});
			});
		}
		public static void DataShaperInjections(this IServiceCollection services)
		{
			services.AddScoped<IDataShaper<BookDto>, DataShaper<BookDto>>();
		}
		public static void AddCustomMediaTypes(this IServiceCollection services)
		{
			services.Configure<MvcOptions>(config =>
			{
				var systemTextJsonOutputFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

				if (systemTextJsonOutputFormatter is not null)
				{
					systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.storeapp.hateoas+json");
				}

				var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
				if (xmlOutputFormatter is not null)
				{
					xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.storeapp.hateoas+json");
				}
			});
			#region CustomMediaTypesAciklama
			/*

			Configure<MvcOptions> metodu, MVC (Model-View-Controller) seçeneklerini yapılandırmak için kullanılır. 
			Bu, MVC'nin çeşitli özelliklerini, davranışlarını ve biçimleyicilerini yapılandırmaya izin verir.

			OutputFormatters koleksiyonu, MVC tarafından kullanılan çıktı biçimleyicilerinin bir listesini içerir.

			Önce, JSON çıktı biçimleyicisi (SystemTextJsonOutputFormatter) alınır ve 
			özel bir medya türü olan "application/vnd.storeapp.hateoas+json" bu biçimleyicinin desteklediği medya türleri arasına eklenir.

			Ardından, XML çıktı biçimleyicisi (XmlDataContractSerializerOutputFormatter) alınır ve 
			aynı özel medya türü bu biçimleyicinin desteklediği medya türleri arasına eklenir.

			Bu genişletme yöntemi, ASP.NET Core uygulamasına HATEOAS (Hypertext As The Engine Of Application State) desteği eklemek için yaygın olarak kullanılır. 
			Bu şekilde, API'nin döndürdüğü yanıtların medya türlerini genişletebilir ve özel medya türlerini destekleyerek API'nin esnekliğini artırabilirsiniz. 
			Bu, özellikle HATEOAS prensiplerini uygularken API'nin keşfedilebilirliğini artırmak ve 
			istemcilere daha fazla kontrol ve esneklik sağlamak için kullanışlı olabilir.
			 */
			#endregion
		}
	}
}
