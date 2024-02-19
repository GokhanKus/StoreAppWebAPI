namespace WebApi.Models
{
	public class BaseEntity : IEntity
	{
		public int Id { get; set; }
		public DateTime CreatedTime { get; set; } = DateTime.Now;
	}
}
