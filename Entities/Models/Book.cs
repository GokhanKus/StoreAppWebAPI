
namespace Entities.Models
{
	public class Book : BaseEntity
	{
		public string Title { get; set; }
		public decimal Price { get; set; }
		public int? CategoryId { get; set; } //foreign key
		public Category? Category { get; set; } // Navigation Property
	}
}
