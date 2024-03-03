using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IDataShaper<T>
	{
		//ExpandoObject: memberlari runtimeda dinamik olarak eklenebilen ve kaldirilabilen bir obje saglar
		IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
		ExpandoObject ShapeData(T entities, string fieldsString);
	}
}
/*
Data Shaping: 
API tuketicisinin, sorgu dizesi araciligiyla talep ettigi nesnenin alanlarini secerek sonuc setini sekillendirmesini saglar
her api'nin ihtiyaci olmayabilir karmasık ve yogun trafik var ise api'mizde o zaman kullanilabilir ve boyle bir implementasyon yapilacaksa;
runtime'da -calisma zamaninda- kod yaziyoruz ve runtime'da nesne uretmek ilgili nesnenin alanlarini secmek ve buna ait donusleri istemciye yapmak maliyetli istir
books?fields = id
books?fields = id, price
books?fields = id, price, title
 */
