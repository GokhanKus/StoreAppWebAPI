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
		public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
		{
			var model = _mapper.Map<Book>(bookDto);
			_manager.BookRepository.CreateOneBook(model);
			await _manager.SaveAsync();
			return _mapper.Map<BookDto>(model); //geriye BookDto donmemiz lazim ama model Book tipinde, o yuzden tekrar mapping..
		}
		public async Task DeleteOneBookAsync(int id, bool trackChanges)
		{
			var book = await _manager.BookRepository.GetOneBookByIdAsync(id, trackChanges);
			if (book is null)
				throw new BookNotFoundException(id); //refactoring saglamis olduk; refactoring, kodun daha kısa ve anlasilir olmasina denir

			_manager.BookRepository.DeleteOneBook(book);
			await _manager.SaveAsync();
		}
		public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
		{
			var books = await _manager.BookRepository.GetAllBooksAsync(trackChanges);
			return _mapper.Map<IEnumerable<BookDto>>(books);
		}
		public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
		{
			var book = await _manager.BookRepository.GetOneBookByIdAsync(id, trackChanges);
			if (book is null)
				throw new BookNotFoundException(id);    //return NotFound(); 404  
			return _mapper.Map<BookDto>(book); //veritabanından(book) BookDto turunde bir verinin donmesi saglandi
		}
		
		public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
		{
			var book = await _manager.BookRepository.GetOneBookByIdAsync(id, trackChanges);
			if (book is null)  
				throw new BookNotFoundException(id);
			var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
			return (bookDtoForUpdate, book);
		}

		public async Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
		{
			_mapper.Map(bookDtoForUpdate, book);
			await _manager.SaveAsync();
		}

		public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
		{
			var entity = await _manager.BookRepository.GetOneBookByIdAsync(id, trackChanges);
			if (entity is null)
				throw new BookNotFoundException(id);

			_mapper.Map(bookDto, entity);

			//entity = _mapper.Map<Book>(bookDto);

			//_manager.BookRepository.Update(entity); 
			//bu satir olacaksa trackchanges false olmali, izlenen nesne degisiklerden sonra Update() olmadan da dogrudan save edilebilir
			//ya da update() kullanilacaksa degisiklikleri izlemeye gerek yok(trackChanges = false), kullanilmayacaksa degisiklikler izlenmeli

			//entity.Title = bookDto.Title;
			//entity.Price = bookDto.Price;
			await _manager.SaveAsync();
		}
	}
}
