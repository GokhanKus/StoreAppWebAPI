using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.ErrorModel
{
	public class ErrorDetails
	{
        public int StatusCode { get; set; }
        public string? Message { get; set; }
		public override string ToString() //ToString() metodunu ezerek davranısını degistirdik 
		{
			//dongusel referansla ilgili hata olabilir (json ignore cycle loop vs..)
			//duyarlılık ve gizlilik: tostring() metodunu ezerek json ciktisi elde etmek istenmeyen veya gizli kalmasi gereken bilgileri spawnlamaya neden olabilir
			//performans: buyuk ve komplex nesnelerin json temsilini istemek maliyetli olabilir
			return JsonSerializer.Serialize(this);//serialize islemi butun classi ilgilendirdigi "this"
		}
	}
}
#region Serialize & Deserialize
/*
 bir nesneyi serialize etmek demek asagidaki gibi bir cikti almak demektir
 {
    "Message": "...",
    "StatusCode": 200
 }
ve deserialize edersek c# nesnesine donusturur 
orn ErrorDetails errorDetails = JsonConvert.DeserializeObject<ErrorDetails>(SerializeJson);
deserilize isleminden sonra
errorDetails.Message ya da errorDetails.StatusCode yazarak ulasilabilir
*/
#endregion
#region Global Hata yonetimi
/*
adı uzerinde uygulamanın herhangi bir yerinde oluşan hataların otomatik olarak yönetilmesini sağlar ve
kullanıcıya daha iyi bir deneyim sunar. Global hata yönetimi genellikle bir hata yakalama mekanizmasıyla sağlanır ve
uygulama genelinde hata durumlarını ele alacak bir yapı kurmayı içerir.

neden kullaniriz;
Daha İyi Kullanıcı Deneyimi: Hataların merkezi bir şekilde ele alınması ve kullanıcıya uygun şekilde sunulması, kullanıcı deneyimini artırır.
Kullanıcılar, uygulama içinde karşılaştıkları hatalarla daha kolay başa çıkabilirler ve bu sayede uygulamayı daha olumlu bir şekilde değerlendirirler.

Güvenilirlik: Global hata yönetimi, uygulamanın daha güvenilir olmasını sağlar. 
Hataların kontrol altına alınması ve uygun şekilde işlenmesi, uygulamanın çökme riskini azaltır.

Kod Temizliği: Her bir endpoint veya metot içinde hata işleme kodlarının tekrar tekrar yazılması yerine,
global bir hata yönetimi sistemi kullanarak kod temizliği sağlanır.

Geliştirme Kolaylığı: Global hata yönetimi, geliştiricilere hata işleme ve hata takibi konularında kolaylık sağlar. 
Tüm hata işleme mantığı merkezi bir yerde toplandığı için, kodu daha kolay yönetilebilir hale getirir.
 */
#endregion
