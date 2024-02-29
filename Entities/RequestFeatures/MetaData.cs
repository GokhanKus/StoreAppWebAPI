using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
	public class MetaData
	{
		public int CurrentPage { get; set; }
		public int TotalPage { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public bool HasPreviousPage => CurrentPage > 1; //bulundugumuz sayfa 1den buyukse onceki sayfa vardır HasPreviousPage=true
		public bool HasNextPage => CurrentPage < TotalPage;//bulundugumuz sayfa TotalPage'den kucukse sonraki sayfa vardir HasNextPage = true
	}
}
//kac sayfanin oldugu, bizim kacinci sayfada oldugumuz, bulundugumuz sayfadan once veya sonra sayfa var mi? gibi sorulara cevap olacak bir class tanimlandi.