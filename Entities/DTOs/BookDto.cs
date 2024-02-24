using System.Runtime.Serialization;

namespace Entities.DTOs
{
	//[Serializable]
	//public record BookDto(int Id, string Title, decimal Price);
	////dto tipleri serialize edilemez, yani ornegin ben butun kitaplari xml formatta getir dersem hata alirim o yuzden Serializable attribute eklenir, 
	//ama o zaman da bu cikti (output) backingfield geliyor karmasik veri.. alttaki gibi yazalim;
	public record BookDto
	{
		public int Id { get; init; }
		public string Title { get; init; }
		public decimal Price { get; init; }
	}
}

