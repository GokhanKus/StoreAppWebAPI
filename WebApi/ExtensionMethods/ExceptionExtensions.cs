﻿using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Net;

namespace WebApi.ExtensionMethods
{
	public static class ExceptionExtensions
	{
		public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //StatusCodes.Status500InternalServerError;(ilerde customize edilecek simdilik 500 atayalım)
					context.Response.ContentType = "application/json";
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature is not null) //null degilse hata gelmis demektir, boyle de yazılabilir contextFeature?.Error is FileNotFoundException
					{
						logger.LogError($"Something went wrong: {contextFeature.Error}");
						await context.Response.WriteAsync(new ErrorDetails()
						{
							StatusCode = context.Response.StatusCode,
							Message = "Internal Server Error"
						}.ToString());
					}
				});
			});
		}
	}
}
#region Yukaridaki seyin aciklamasi
/*
app.UseExceptionHandler() Metodu: 
Bu, uygulamanın middleware pipeline'ına bir hata işleyici(ExceptionHandler) ekler. 
Bu metod, uygulamada bir hata oluştuğunda bu hataları ele almak için kullanılır.

appError.Run Metodu: 
Bu, hata işleyicisinin asıl çalışma mantığını tanımlar. Yani, bir hata oluştuğunda ne yapılacağını belirtir. İlgili logic'i bu metot icerisinde tanimlariz.

context.Response Ayarları: 
Bu satırlar, HTTP yanıtının durum kodunu ve içerik türünü ayarlar. 
Bu durumda, hata oluştuğunda yanıtın 500 (Internal Server Error) durum kodu ve JSON içerik türü olacağı belirtilir.

context.Features.Get<IExceptionHandlerFeature>(): 
Bu satır, hata bilgilerini almak için kullanılır. IExceptionHandlerFeature arayüzünün bir örneği alınır ve hata bilgileri bu özellikten elde edilir.

Hata Loglama: 
Eğer hata bilgileri mevcutsa (contextFeature null değilse), bu hata bilgileri loglanır.
Loglama işlemi, ILoggerService arayüzünü uygulayan bir servis aracılığıyla yapılır.

HTTP Yanıtı Oluşturma: 
Son olarak, hata bilgileri JSON formatına dönüştürülür ve HTTP yanıtına yazılır. Bu, istemciye döndürülecek olan hata bilgilerini içeren JSON yanıtını oluşturur.
 */
#endregion