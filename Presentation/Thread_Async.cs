using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	internal class Thread_Async
	{
	}
	
}
/*
Async => 3'e ayrilir
async programming model (APM)
event-based async pattern (EAP)
task-based async pattern (TAP) bununla ilgilenecegiz

istemcilerin requestleri olur ve bu istek pipeline'daki bloklardan gecerek(cache, authentication, cors, dbcontext, http handlers vs.) bize bir response uretir

ve burada senkron bir surec yurutuluyor ise 
o pipelinedaki islemlerin tamami BİR thread icerisinde olur ve o threadin bir id'si olur örn Thread: 29232 vs gibi
ve her sey o thread icerisinde olur ve biter

3 client olsun 3 tane sync request gelsin, thread pool 2 thread olsun
baska bir deyisle;
senkron bir islem var ise requesti aliyoruz ve threadpool'dan thread'i olusturup ilgili ifadeyi thread icerisinde cözüp ilgili cevabi uretiyoruz
baska bir istek gelirse yine threadpool'dan thread olusturup ayni islem gerceklesir,
ama eger Thread havuzu dolduysa yani yeni thread uretilemiyorsa ondan sonra gelen 3.istekte diger ilk 2 istegin isleminin bitmesini beklemek zorunda  

asenktron bir surec yurutuluyor ise, yine
3 client olsun 3 tane async request gelsin, thread pool 3 thread olur (kac request varsa her biri ayri thread uzerinde) 
bir islem(request) gerceklesirken bir sonraki islemin(request) diger threadi beklemesine gerek yoktur, islemler ayri ayri threadler uzerinde donebilir(sync ile farkı)

bu bize performans saglar, 
ANCAK 1 client 1 request gondersin ornegin bir veritabani islemi.. bunun sync veya async olması farketmez, ayni miktarda sistem kaynagi tuketilir ayni miktarda beklenir,
ama bloklama sureleri vardir örn dosyalama islemi olsun dosyaya yazma islemi olsun oradaki bloklama suresini beklemek istemeyebiliriz ya da
paralelinde baska islem yapmak isteyebiliriz bu gibi senaryolarda performans artisi olabilir, 
ama spesifik bir is yapilacaksa o isin bellek tuketimi veya zamanlama acisindan tamamlanmasi sync veya async olarak yazilmasi farketmez

istemcilerin requestleri olur ve bu istek pipeline'daki bloklardan gecerek(cache, authentication, cors, dbcontext, http handlers vs.) bize bir response uretir
ve burada asenkron bir surec yurutuluyor ise 
o pipelinedaki islemlerin(middlewareler) her biri ayri ayri(cache, cors, dbcontext) threadler icerisinde yurutulebilir Thread:2534 begin, Thread: 2562 begin Thread:2324 begin vs
yani bir taraftan orn caching islmeni yapilirken diger taraftan dbcontextle ilgili bir is yapilabiliyor, dolayisiyla threadler birbirini beklemedigi icin
Task anahtar sozcuguyle async islemlerimizi yapariz 
Task hem thread hem de asenkron yonetim surecini bizim yerimize framework uzerinden gerceklestiriyor
task objesi ustlendigi isleri threadpool uzerinden asenktron olarak calistirma gorevini ustlenir

.Net dunyasinda asenktron programlama icin threadpool kullaniliyor ve Threadler threadpooldan çekilir
calistirilan thread yapilari icin task kullanilir
task objesi temel olarak ustlendigi isleri thread pool uzerinde asenkron olarak calistirir
task icerisinde status, iscompleted, iscanceled, isfaulted gibi cok sayida property vardir ve task ile biz operasyonun tamamlanip tamamlanmadigini izleyebiliriz

bir metot, bir operasyon async olarak tanimlandiysa await anahtar sozgucu o ilgili ifadenin asenkron olarak calismasindan sorumludur ve metodun, operasyonun basarısını dogrular
zaman uyumsuz yontemde kodun geri kalanını yurutmek icin devamı saglar 

//yukaridakinin Chatgpt outputu;
Kavram				  Açıklama
Async				: Asenkron programlama modeli, 3'e ayrılır: async programming model (APM), event-based async pattern (EAP), task-based async pattern (TAP). Biz task-based async pattern'e odaklanacağız.
Pipeline			: İstemcilerin requestleri, pipeline'daki bloklardan geçerek (cache, authentication, cors, dbcontext, http handlers vs.) bir response üretir.
Senkronizasyon		: Senkron bir işlemde, tüm pipeline işlemleri tek bir thread içinde gerçekleşir.
Asenkronizasyon		: Asenkron bir işlemde, her işlem (request) ayrı bir thread üzerinde gerçekleşir.
Thread Pool			: Thread havuzu, belirli sayıda thread'in oluşturulup kullanıldığı bir yapıdır.
Performans			: Asenkron işlemler performansı artırır, ancak tek bir client için tek bir request gönderildiğinde performans farkı olmayabilir.
Task				: Task, asenkron yönetimi sağlar ve işleri thread pool üzerinde asenkron olarak çalıştırır.
Await				: Async olarak tanımlanan bir operasyonun tamamlanmasını bekler ve metodun devamını sağlar.

Async, asenkron programlama modeli olup 3'e ayrılır: 
async programming model (APM), 
event-based async pattern (EAP), 
task-based async pattern (TAP). Biz task-based async pattern'e odaklanacağız.

İstemcilerin requestleri pipeline'daki bloklardan geçerek (cache, authentication, cors, dbcontext, http handlers vs.) bir response üretir. 
Senkron bir işlemde, tüm pipeline işlemleri tek bir thread içinde gerçekleşir.

Asenkron bir işlemde, her işlem (request) ayrı bir thread üzerinde gerçekleşir. 
Örneğin, 3 client olsun ve 3 tane async request gelsin, bu durumda thread pool 3 thread'e sahip olur (her biri bir request'i işler).

Asenkron işlemler performansı artırır, ancak tek bir client için tek bir request gönderildiğinde performans farkı olmayabilir.
Ancak, belirli senaryolarda (örneğin, dosyalama işlemleri) performans artışı olabilir.

Task, asenkron yönetimi sağlar ve işleri thread pool üzerinde asenkron olarak çalıştırır. 
Task objesi, belirli işlemleri asenkron olarak gerçekleştirir ve birçok faydalı özelliği içerir (örneğin, status, isCompleted, isCanceled, isFaulted).

Async olarak tanımlanan bir operasyonun tamamlanmasını beklemek için await kullanılır. Bu, zaman uyumsuz yontemde kodun geri kalanını yürütmek için devamı sağlar.
 */