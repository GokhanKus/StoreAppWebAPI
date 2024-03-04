using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
	public class Link
	{
		public string? Href { get; set; }
		public string? Rel { get; set; }
		public string? Method { get; set; }
        public Link()
        {
            
        }
		public Link(string? href, string? rel, string? method)
		{
			Href = href;
			Rel = rel;
			Method = method;
		}
	}
}
#region Link.cs ve proplari
/*
Bu sınıf, bir API'nin sunduğu kaynaklar arasındaki ilişkileri ve bu kaynaklara erişmek için gereken bilgileri temsil eder.

Href: Bu özellik, ilgili kaynağın URI'sini (Uniform Resource Identifier - Benzersiz Kaynak Tanımlayıcısı) içerir. 
İlgili kaynağa erişmek için kullanılacak olan URL'yi belirtir.

Rel: Bu özellik, ilişkiyi (relationship) tanımlar. İlgili kaynağın, mevcut kaynağa olan ilişkisini belirtir. 
Örneğin, "self" değeri, ilgili kaynağın kendisiyle ilişkilendirildiğini gösterebilir.

Method: Bu özellik, HTTP isteğinin metodunu belirtir. Kaynağa erişmek için kullanılacak olan HTTP metodunu belirtir.
Örneğin, "GET", "POST", "PUT" veya "DELETE" gibi.

Link(): Bu bir yapıcı metoddur. Parametre olarak href, rel ve method alır ve bu değerleri ilgili sınıf özelliklerine atar. 
Bu, bir Link örneği oluştururken kullanılabilir.
 */
#endregion
#region Hateoas Hypermedia
/*
RESTful API (Representational State Transfer) 
bir tür web servisidir ve HTTP protokolünü kullanarak kaynaklara (resources) erişim sağlar.
Bu tür bir API, web üzerindeki kaynaklara (veritabanı kaynakları, dosyalar, servisler vb.) standart HTTP methodlarıyla (GET, POST, PUT, DELETE vb.) erişmeyi sağlar.

restful apilerin olgunlasma seviyeleri vardir level 0'dan level 3'e kadar cogu api level 2'dir(http verbleri get, put, post vs. kullanilan level 2)
level 3 api'de hateoas(Hypermedia as the engine of application state) bulunur

hypermedialar -hateoas- istemcinin, yani api'yi kullanacaklarin apiyi kesfetmesini saglar,
API tarafından sunulan bir kaynağa yapılan bir istek, aynı zamanda diğer ilişkili kaynaklara nasıl erişileceği hakkında da bilgi içerir.
hypermedia destegi eger apimiz cok yogun ise ve bircok kisi tarafından kullanilacaksa verilmelidir aksi halde vermeye gerek yoktur ve ilerleyen zamanlarda da bu destek verilebilir.

HATEOAS (Hypermedia As The Engine Of Application State), API'lerin kullanım kılavuzu gibi düşünülebilir. 
HATEOAS, bir API'nin kendini belgelemesi ve keşfedilmesini kolaylaştıran bir prensiptir.
 */
#endregion