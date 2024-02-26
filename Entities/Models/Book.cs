
namespace Entities.Models
{
	public class Book:BaseEntity
	{
        public string Title { get; set; }
        public decimal Price { get; set; }

		public static explicit operator Book(Task<Book> v)
		{
			throw new NotImplementedException();
		}
	}
}
