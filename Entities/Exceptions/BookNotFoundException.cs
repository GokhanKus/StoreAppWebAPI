namespace Entities.Exceptions
{
	public sealed class BookNotFoundException : NotFoundException //BookNotFound'u sealed oldugu kalıtım ile devralamazsin
	{
		public BookNotFoundException(int id) : base($"The book with id: {id} could not found")
		{

		}
	}
}
