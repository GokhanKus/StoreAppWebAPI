namespace Entities.Exceptions
{
	public sealed class BookNotFound : NotFound //BookNotFound'u sealed oldugu kalıtım ile devralamazsin
	{
		public BookNotFound(int id) : base($"The book with id: {id} could not found")
		{

		}
	}
}
