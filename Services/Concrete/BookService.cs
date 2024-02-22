using Entities.Exceptions;
using Entities.Models;
using Repositories.RepoContracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
	public class BookService : IBookService
	{
		private readonly IRepositoryManager _manager;
		private readonly ILoggerService _logger;
		public BookService(IRepositoryManager manager, ILoggerService logger)
		{
			_manager = manager;
			_logger = logger;
		}
		public Book CreateOneBook(Book book)
		{
			_manager.BookRepository.CreateOneBook(book);
			_manager.Save();
			return book;
		}
		public void DeleteOneBook(int id, bool trackChanges)
		{
			var book = _manager.BookRepository.GetOneBookById(id, trackChanges);
			if (book is null)
				throw new BookNotFoundException(id); //refactoring saglamis olduk; refactoring, kodun daha kısa ve anlasilir olmasina denir

			_manager.BookRepository.DeleteOneBook(book);
			_manager.Save();
		}
		public IEnumerable<Book> GetAllBooks(bool trackChanges)
		{
			return _manager.BookRepository.GetAllBooks(trackChanges);
		}
		public Book? GetOneBookById(int id, bool trackChanges)
		{
			var book = _manager.BookRepository.GetOneBookById(id, trackChanges);
			if (book is null)
				throw new BookNotFoundException(id);    //return NotFound(); 404
			return book;
		}
		public void UpdateOneBook(int id, Book book, bool trackChanges)
		{
			var entity = _manager.BookRepository.GetOneBookById(id, trackChanges);
			if (entity is null)
				throw new BookNotFoundException(id);

			entity.Title = book.Title;
			entity.Price = book.Price;

			//_manager.BookRepository.Update(entity); izlenen nesne degisiklerden sonra Update() olmadan da dogrudan save edilebilir
			_manager.Save();
		}
	}
}
