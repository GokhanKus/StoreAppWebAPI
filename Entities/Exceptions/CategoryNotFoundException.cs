namespace Entities.Exceptions
{
	public sealed class CategoryNotFoundException : NotFoundException //sealed oldugu kalıtım ile devralamazsin
	{
		public CategoryNotFoundException(int id) : base($"The category with id: {id} could not found")
		{

		}
	}
}
