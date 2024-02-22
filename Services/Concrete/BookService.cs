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
			{
				string message = $"the book with id:{id} could not found";
				_logger.LogInfo(message);
				throw new Exception(message);
			}
		
			_manager.BookRepository.DeleteOneBook(book);
			_manager.Save();
		}
		public IEnumerable<Book> GetAllBooks(bool trackChanges)
		{
			return _manager.BookRepository.GetAllBooks(trackChanges);
		}
		public Book? GetOneBookById(int id, bool trackChanges)
		{
			return _manager.BookRepository.GetOneBookById(id, trackChanges);
		}
		public void UpdateOneBook(int id, Book book, bool trackChanges)
		{
			var entity = _manager.BookRepository.GetOneBookById(id, trackChanges);
			if (entity is null)
			{
				string msg = $"the book with id:{id} could not found";
				_logger.LogInfo(msg);
				throw new Exception(msg);
			}
			
			entity.Title = book.Title;
			entity.Price = book.Price;

			//_manager.BookRepository.Update(entity); izlenen nesne degisiklerden sonra Update() olmadan da dogrudan save edilebilir
			_manager.Save();
		}
	}
}
