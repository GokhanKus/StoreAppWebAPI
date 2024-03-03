using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{//bundan sonra butun kaynaklarda(book gibi diger olusturulacak olan tablolarda) bu class kalıtım alınarak kullanılabilir ortak proplar burada tanımlanacak 
 //ve base class oldugu icin abstract
	public abstract class RequestParameters
	{
		const int maxPageSize = 50; //istemciye max 50 kaynak verilsin

		//auto-implemented property
		public int PageNumber { get; set; }
		public string? SearchingTerm { get; set; }
		public string? OrderBy { get; set; }
		//full-property
		private int _pageSize;
		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = value > maxPageSize ? maxPageSize : value; }
			//value 50den buyukse(istemci 50'den fazla kaynak isterse max 50, 50'den kucukse istenilen deger gelsin)
		}
	}
}
/*
Pagination:
paging API'dan sonuclarin kısmi olarak alinmasidir ve RESTful API icin onemli bir ozelliktir. 
örnegin banka musterisiyiz ve para hareketleri ile ilgili bilgi talep ediyoruz ve 1000lerce 10000lerce data donebilir
ve bu datanın tamamını istemciye gondermek mantikli degil performansi kotu etkiler
*/