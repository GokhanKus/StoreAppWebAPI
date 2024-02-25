using AutoMapper;
using Entities.DTOs;
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
		private readonly IMapper _mapper;
		public BookService(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
		{
			_manager = manager;
			_logger = logger;
			_mapper = mapper;
		}
		public BookDto CreateOneBook(BookDtoForInsertion bookDto)
		{
			var model = _mapper.Map<Book>(bookDto);
			_manager.BookRepository.CreateOneBook(model);
			_manager.Save(); 
			return _mapper.Map<BookDto>(model); //geriye BookDto donmemiz lazim ama model Book tipinde, o yuzden tekrar mapping..
		}
		public void DeleteOneBook(int id, bool trackChanges)
		{
			var book = _manager.BookRepository.GetOneBookById(id, trackChanges);
			if (book is null)
				throw new BookNotFoundException(id); //refactoring saglamis olduk; refactoring, kodun daha kısa ve anlasilir olmasina denir

			_manager.BookRepository.DeleteOneBook(book);
			_manager.Save();
		}
		public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
		{
			var books = _manager.BookRepository.GetAllBooks(trackChanges);
			return _mapper.Map<IEnumerable<BookDto>>(books);
		}
		public BookDto GetOneBookById(int id, bool trackChanges)
		{
			var book = _manager.BookRepository.GetOneBookById(id, trackChanges);
			if (book is null)
				throw new BookNotFoundException(id);    //return NotFound(); 404  
			return _mapper.Map<BookDto>(book); //veritabanından(book) BookDto turunde bir verinin donmesi saglandi
		}
		public void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges)
		{
			var entity = _manager.BookRepository.GetOneBookById(id, trackChanges);
			if (entity is null)
				throw new BookNotFoundException(id);

			_mapper.Map(bookDto, entity);

			//entity = _mapper.Map<Book>(bookDto);

			//_manager.BookRepository.Update(entity); 
			//bu satir olacaksa trackchanges false olmali, izlenen nesne degisiklerden sonra Update() olmadan da dogrudan save edilebilir
			//ya da update() kullanilacaksa degisiklikleri izlemeye gerek yok(trackChanges = false), kullanilmayacaksa degisiklikler izlenmeli
			
			//entity.Title = bookDto.Title;
			//entity.Price = bookDto.Price;
			_manager.Save();
		}
	}
}
