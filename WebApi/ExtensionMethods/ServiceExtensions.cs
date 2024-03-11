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
using Microsoft.OpenApi.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.Controllers;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using AspNetCoreRateLimit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

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
			services.AddScoped<ValidateMediaTypeAttribute>();
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
		public static void BookLinkInjections(this IServiceCollection services)
		{
			services.AddScoped<IBookLinks, BookLinks>();
		}
		public static void AddCustomMediaTypes(this IServiceCollection services)
		{
			services.Configure<MvcOptions>(config =>
			{
				var systemTextJsonOutputFormatter = config.OutputFormatters.OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

				if (systemTextJsonOutputFormatter is not null)
				{
					systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.storeapp.hateoas+json");
					systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.storeapp.apiroot+json");
				}

				var xmlOutputFormatter = config.OutputFormatters.OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
				if (xmlOutputFormatter is not null)
				{
					xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.storeapp.hateoas+xml");
					xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.storeapp.apiroot+xml");
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
		public static void ConfigureVersioning(this IServiceCollection services)
		{
			services.AddApiVersioning(opt =>
			{
				opt.ReportApiVersions = true; //apinin version bilgisini response header bolumune ekliyoruz
				opt.AssumeDefaultVersionWhenUnspecified = true; //user any bir version bilgisi talep etmezse, apinin default versionu ile (1.0) donus yapmis olacak
				opt.DefaultApiVersion = new ApiVersion(1, 0); //1.0 surumu
				opt.ApiVersionReader = new HeaderApiVersionReader("api-version");

			}).AddMvc(options =>
			{
				options.Conventions.Controller<BooksController>().HasApiVersion(new ApiVersion(1, 0));
				options.Conventions.Controller<BooksV2Controller>().HasDeprecatedApiVersion(new ApiVersion(2, 0));
			});//AddMvc() eklemeyince hata veriyordu paket farklı oldugu icin..
			   //Microsoft.AspNetCore.Mvc.Versioning kurmadik, cunku deprecated(kullanimdan kaldirildi) onun yerine Asp.Versioning.Mvc package kuruldu
		}
		public static void ConfigureResponseCaching(this IServiceCollection services)
		{
			services.AddResponseCaching();//location belirtilmezse default degeri any'dir yani hem client hem de proxydir.
										  //postman'de settings'te send no cache header ayarı off yap
			#region Caching
			/*
			Expiration Model
			Cache mekanizmasi client servera ilk kez bir request atarken cachable ise expiration modelde cache'de tutar ve cliente veriyi response eder.
			(ornegin: max-age:60 (60 sn cachede duracak vs.))
			ve client tekrar ayni requesti atarken bu sefer data api'den değil, cache'den gelmis olacak boylece api uzerinden yuku azaltmis oluyoruz.
			60sn'den sonra tekrar ayni request gelirse bu request fresh olmadigi icin tekrar api'ye gider

			bu cachelerin saklandigi, depolandigi yer olmasi lazim 3 yaklasim var;
			1- client cache: istemcinin browserinda cacheleme mekanizmasinda o response saklanir
			2- gateway cache: server tarafında saklanır orn client1 api/books talep etti sunucu tarafında bu responce cachelenip diger clientlar ile de paylasilabilir yani 
			client2'de aynı kaynagi talep ederse servera gitmez cacheden yanit alir bu da paylasilabilir olan guzel bir ozelliktir
			3- proxy cache: cacheleme islemi network uzerinde yapilir 

			Validation Model
			Expiration modeldeki gibidir, ama burada da ilk kez bir request attıktan sonra response header kisminda ETag attribute ve last-modified date bilgileri ile veriler tutulur.
			orn: ETag:131523453 Last-Modified: Mon, 15 Oct 2023 11.20 GMT vs.
			ve client tekrar ayni requesti attiginda bu kaynak hala fresh ise yani hala cache icerisinde ise 
			304 not modified ile yeniden kaynak olusturmadan ilgili kaynagin fresh oldugunu ifade ederek cliente response edecegiz
			 */
			#endregion
		}
		public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
		{
			services.AddHttpCacheHeaders(expirationOpt =>
			{
				expirationOpt.MaxAge = 70;// 70 saniye
				expirationOpt.CacheLocation = CacheLocation.Public;//bunu private yaparsak controller'da [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 80)] yazmalıyız
			}, validationOpt =>
			{
				validationOpt.MustRevalidate = false;
			});
			//ETag, Expires, Last-Modified attributeleri response headers bolumune gelir
			//alternatif cacheleme yontemleri: varnish
		}
		public static void ConfigureRateLimiting(this IServiceCollection services)
		{
			var rateLimitRules = new List<RateLimitRule>
			{
				new RateLimitRule
				{
					Endpoint = "*", //tum endpointlere uygulansin, endpointlerin tamamini kapsasin
					Limit = 3,
					Period = "1m" //1 dk'da max 3 request, fazlası too many request 429 
				}
			};
			services.Configure<IpRateLimitOptions>(opt =>
			{
				opt.GeneralRules = rateLimitRules; //genel ratelimit kurallarini iceren bir listeyi temsil ederç
			});

			services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();	//Bu, rate limit istek sayacının bellekte saklanacağını belirtir.
			services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();					//Bu, IP adresi tabanlı politikaların bellekte saklanacağını belirtir.
			services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();			//Bu, genel rate limit yapılandırmasını sağlar.
			services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();//Rate limit isteklerinin nasıl işleneceğini belirten bir işlem stratejisi eklenir.Bu örnekte, AsyncKeyLockProcessingStrategy kullanılmaktadır.
		}
		public static void ConfigureIdentityDbContext(this IServiceCollection services)
		{
			services.AddIdentity<User, IdentityRole>(opt =>
			{
				opt.Password.RequireDigit = true; //kayit islemi sırasinda rakam zorunlulu
				opt.Password.RequireUppercase = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireNonAlphanumeric = false; //& % + gibi karakterler zorunlu olmasin
				opt.Password.RequiredLength = 6; //min 6 karakter
				opt.User.RequireUniqueEmail = true;//mailler unique olsun her userin maili kendine ait olsun vs.
				opt.SignIn.RequireConfirmedAccount = false; //kayit isleminden sonra e posta onaylama zorunlulugu olmasin

			})
				.AddEntityFrameworkStores<RepositoryContext>()
				.AddDefaultTokenProviders(); //jwt kullanacagiz ve sifre, mail, resetleme, degistirme, mail onaylama gibi islemler icin gereken token bilgisini üretmek icin AddDefaultTokenProviders().
		}
	}
}
