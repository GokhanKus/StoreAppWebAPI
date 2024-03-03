using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
	public class DataShaper<T> : IDataShaper<T> where T : class
	{
		public PropertyInfo[] Properties { get; set; }
		public DataShaper()
		{
			Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
		}
		public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string? fieldsString)
		{
			var requiredProperties = GetRequiredProperties(fieldsString);
			return FetchDataForEntity(entities, requiredProperties);
		}

		public ExpandoObject ShapeData(T entity, string fieldsString)
		{
			var requiredProperties = GetRequiredProperties(fieldsString);
			return FetchDataForEntity(entity, requiredProperties);
		}
		private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldString)
		{
			var requiredFields = new List<PropertyInfo>();
			if (!string.IsNullOrWhiteSpace(fieldString))
			{
				var fields = fieldString.Split(',', StringSplitOptions.RemoveEmptyEntries); //books?field=id,,title,, gibi query girerse oncelikle alanlari ayiralim "id" "title"
				foreach (var field in fields)
				{
					PropertyInfo? property = Properties
						.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
					if (property is null)
						continue;
					requiredFields.Add(property);
				}
			}
			else
			{
				requiredFields = Properties.ToList();//eger client hicbir field sorgusu yazmadiysa butun alanlarin getirilmesini istiyor demektir o yuzden hepsini aldik.
			}
			return requiredFields;
		}
		private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
		{
			var shapedObject = new ExpandoObject();
			foreach (var property in requiredProperties)
			{
				var objectPropertyValue = property.GetValue(entity);
				shapedObject.TryAdd(property.Name, objectPropertyValue);
			}
			return shapedObject;
		}
		private IEnumerable<ExpandoObject> FetchDataForEntity(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
		{
			var shapedData = new List<ExpandoObject>();
			foreach (var entity in entities)
			{
				var shapedObject = FetchDataForEntity(entity, requiredProperties);
				shapedData.Add(shapedObject);
			}
			return shapedData;
		}
	}
}
#region Reflection
/*
Reflection(Yansıma), bir programın kendi yapısını çalışma zamanında inceleme, analiz etme ve hatta değiştirme yeteneğidir. 
Bu, bir programın derleme zamanında bilinen türleri, özellikleri ve metotları hakkında bilgi almanıza, 
bu türlerin özelliklerine ve metotlarına erişmenize, hatta bunları çalışma zamanında oluşturmanıza ve kullanmanıza olanak tanır.

C# ve .NET gibi modern programlama dilleri ve çerçeveler, Reflection özelliğini desteklerler.
Reflection, genellikle dinamik olarak türleri, özellikleri ve metotları kullanmanın gerektiği durumlarda kullanılır.
Örneğin, veritabanı sorgularını oluşturmak, XML veya JSON verilerini serileştirmek/deserileştirmek, 
dinamik olarak türler oluşturmak veya mevcut türlerin özelliklerini sorgulamak gibi durumlarda Reflection kullanılabilir.

Reflection, System.Reflection adlı .NET kütüphanesinde yer alan sınıflar ve yöntemler tarafından sağlanır. 
Bu sınıflar ve yöntemler, türleri ve bunların üyelerini (örneğin, alanlar, özellikler, metotlar) analiz etmenize ve 
çalışma zamanında bu türlerle etkileşimde bulunmanıza olanak tanır.

Birkaç yaygın Reflection kullanımı şunlardır:

Bir türün adını kullanarak bir türü dinamik olarak yükleme ve kullanma.
Bir türün özelliklerine ve metotlarına dinamik olarak erişme ve onları çağırma.
Bir türün alanlarını ve özelliklerini sorgulama ve değerlerini okuma/yazma.
Mevcut türlerin üyeleri hakkında bilgi alma ve bu bilgileri kullanma.
Ancak, Reflection güçlü bir araç olmasına rağmen, aşırı kullanımı performansı etkileyebilir ve kodunuzu karmaşık hale getirebilir. 
Bu nedenle, Reflection'u yalnızca gerektiğinde ve uygun bir şekilde kullanmak önemlidir.
*/
#endregion
#region Data Shaping
/*
Data Shaping: 
API tuketicisinin, sorgu dizesi araciligiyla talep ettigi nesnenin alanlarini secerek sonuc setini sekillendirmesini saglar
her api'nin ihtiyaci olmayabilir karmasık ve yogun trafik var ise api'mizde o zaman kullanilabilir ve boyle bir implementasyon yapilacaksa;
runtime'da -calisma zamaninda- kod yaziyoruz ve runtime'da nesne uretmek ilgili nesnenin alanlarini secmek ve buna ait donusleri istemciye yapmak maliyetli istir
books?fields = id
books?fields = id, price
books?fields = id, price, title
*/
#endregion
