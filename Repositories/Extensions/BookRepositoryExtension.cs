using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
	public static class BookRepositoryExtension
	{
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
	}
}
