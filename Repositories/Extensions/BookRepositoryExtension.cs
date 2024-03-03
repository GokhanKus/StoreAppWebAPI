using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq.Dynamic.Core;

namespace Repositories.Extensions
{
	public static class BookRepositoryExtension
	{
		//Bu paginate metodu kullanilmiyor artik, cunku bunun icin Entities katmaninda PagedList.cs yazdik.
		public static IQueryable<Book> Paginate(this IQueryable<Book> books, int pageNumber, int pageSize)
		{
			return books
				.Skip((pageNumber - 1) * pageSize) // IIIII IIIII IIIII örnegin 3.sayfaya gitmek istersem (3-1) * 5 = 10 tane item atlayacagimi soyluyorum
				.Take(pageSize);
		}
		public static IQueryable<Book> FilterBooksWithPrice(this IQueryable<Book> books, uint minPrice, uint maxPrice)
		{
			return books.Where(b => ((b.Price >= minPrice) && b.Price <= maxPrice));
		}
		public static IQueryable<Book> FilterBooksWithName(this IQueryable<Book> books, string? searchingTerm)
		{
			if (string.IsNullOrEmpty(searchingTerm))
				return books;
			return books.Where(b => b.Title.ToLower().Contains(searchingTerm.Trim().ToLower()));
		}
		public static IQueryable<Book> Sort(this IQueryable<Book> books, string? orderByQueryString)//books?orderBy = title, price,
		{
			if (string.IsNullOrEmpty(orderByQueryString))//eger sıralamayla ilgili sorgu yoksa default id'ye gore siralasin
				return books.OrderBy(b => b.Id);

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

			if (orderQuery is null)
				return books.OrderBy(b => b.Id);

			return books.OrderBy(orderQuery); //title ascending, price descending, id ascending'ye gore sirala
		}
	}
}
