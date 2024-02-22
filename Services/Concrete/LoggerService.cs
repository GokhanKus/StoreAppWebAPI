using NLog;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
	public class LoggerService : ILoggerService
	{
		private static ILogger logger = LogManager.GetCurrentClassLogger();

		public void LogDebug(string message) => logger.Debug(message);
		public void LogError(string message) => logger.Error(message);
		public void LogInfo(string message) => logger.Info(message);
		public void LogWarning(string message) => logger.Warn(message);
	}
}
/*
Loglama servisleri genellikle bir uygulamanın çalışma zamanında oluşan olayları, hataları veya bilgilendirici mesajları kaydetmek için kullanılır. 
Bu, uygulamanın hata ayıklanması, performansın izlenmesi ve genel olarak uygulamanın durumu hakkında bilgi edinilmesi için önemli bir araçtır.
Sınıfın içerisindeki LogDebug, LogError, LogInfo ve LogWarning gibi yöntemler, farklı log düzeylerinde mesajların kaydedilmesini sağlar:

LogDebug: Hata ayıklama sürecinde kullanılan bilgi düzeyindeki mesajları kaydeder.
LogError: Uygulamada bir hata meydana geldiğinde kaydedilmesi gereken mesajları belirler.
LogInfo: Uygulamanın normal çalışması sırasında kaydedilmesi gereken bilgilendirici mesajları sağlar.
LogWarning: Dikkat edilmesi gereken potansiyel sorunları belirten mesajları kaydeder.
*/