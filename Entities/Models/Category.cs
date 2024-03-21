using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
	public class Category : BaseEntity
	{
		public string? CategoryName { get; set; }

		//Ref: Navigation Property
		//public ICollection<Book>? Books { get; set; }
		//bunu yazmasak da olur; yani kitaplarin listesini getirirken bir kitabin ait oldugu category gelirken,
		//o kategoriye ait diger kitaplar da geliyor bunu istemeyebiliriz.
	}
}
