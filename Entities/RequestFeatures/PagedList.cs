using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
	public class PagedList<T> : List<T>
	{
		public MetaData MetaData { get; set; }
		public PagedList(List<T> items, int count, int pageNumber, int pageSize)//List<T> items, ornegin List<Book> Books (IEnumerable<Book> Books) olarak dusunulebilir 
		{
			MetaData = new MetaData
			{
				TotalCount = count,
				CurrentPage = pageNumber,
				PageSize = pageSize,
				TotalPage = (int)Math.Ceiling(count / (double)pageSize)
			};
			AddRange(items); //List<T>'de gelen degerler PagedList<T>'e tasinacak
		}
		public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
		{
			var count = source.Count();
			var items = source
				.Skip((pageNumber - 1) * pageSize) // IIIII IIIII IIIII örnegin 3.sayfaya gitmek istersem (3-1) * 5 = 10 tane item atlayacagimi soyluyorum
				.Take(pageSize)
				.ToList();
			return new PagedList<T>(items, count, pageNumber, pageSize);
		}
	}
}
