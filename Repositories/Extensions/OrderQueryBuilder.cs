using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
	public static class OrderQueryBuilder
	{
		//BookRepository'deki kodu buraya aldik cunku farklı entityler icin sıralama yapmak isteyebiliriz hepsi bunu kullansin hepsi icin ayri ayri yazilmasin
		public static string CreateOrderQuery<T>(string orderByQueryString)
		{
			//query'de orn books?orderBy=title, price, id gibi alanlar varsa ","'e gore ayiracagiz. Array'de => 0: title, 1:price, 2:id olacak
			var orderParams = orderByQueryString.Trim().Split(','); //queryString uzerinden var olan queryleri aldik

			//Book class'ina ait(Id,Title,Price,CreatedTime) public memberlar ya da newlenebilen(static olmayan) ornekleri alalım
			var propertyInfos = typeof(Book).GetProperties(BindingFlags.Public | BindingFlags.Instance);//class uzerinden prop bilgilerini aldik

			var orderQueryBuilder = new StringBuilder();

			//ornegin dongu sonunda title ascending, price descending, id ascending, olabilir
			foreach (var param in orderParams)
			{
				if (string.IsNullOrEmpty(param))
					continue;
				//once ","'e gore queryleri ayirdik sonra bu ifadelerde bosluk olanlari ayirdik. orn books?orderBy = title, price desc, price ve desc
				var propertyFromQueryName = param.Split(' ')[0]; //ornegin price desc veya id asc gibi query gelirse price, id alanini alalim

				var objectProperty = propertyInfos //price, title gibi alanlar 
					.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

				if (objectProperty is null)
					continue;

				var direction = param.EndsWith(" desc") ? "descending" : "ascending";
				orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
			}
			var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
			return orderQuery;
		}
	}
}
