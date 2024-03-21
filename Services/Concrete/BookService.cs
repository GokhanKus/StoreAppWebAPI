using AutoMapper;
using Entities.DTOs;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Entities.RequestFeatures;
using Repositories.RepoContracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
	public class BookService : IBookService
	{
		private readonly IRepositoryManager _manager;
		//private readonly ILoggerService _logger;
		private readonly IMapper _mapper;
		private readonly IBookLinks _bookLinks;
		private readonly ICategoryService _categoryService; //bookservice'de category ile islem yapacaksak _manager.CategoryRepository ile repo'ya gitmek yerine category service'ye gidelim
		public BookService(IRepositoryManager manager, IBookLinks bookLinks, IMapper mapper, ICategoryService categoryService)
		{
			_manager = manager;
			//_logger = logger;
			_mapper = mapper;
			_bookLinks = bookLinks;
			_categoryService = categoryService;
		}
		public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
		{
			//TODO: create isleminde categoryid yaptigimiz gibi update icin de yap
			var category = await _categoryService.GetOneCategoryByIdAsync(bookDto.CategoryId, false);

			var model = _mapper.Map<Book>(bookDto);
			_manager.BookRepository.CreateOneBook(model);
			await _manager.SaveAsync();
			return _mapper.Map<BookDto>(model); //geriye BookDto donmemiz lazim ama model Book tipinde, o yuzden tekrar mapping..
		}
		public async Task DeleteOneBookAsync(int id, bool trackChanges)
		{
			var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

			_manager.BookRepository.DeleteOneBook(book);
			await _manager.SaveAsync();
		}
		public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges)
		{
			if (!linkParameters.BookParameters.IsValid)
				throw new PriceOutOfRangeBadRequestException();

			var booksWithMetaData = await _manager.BookRepository.GetAllBooksAsync(linkParameters.BookParameters, trackChanges);
			var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);
			var links = _bookLinks.TryGenerateLinks(booksDto, linkParameters.BookParameters.Fields, linkParameters.HttpContext);
			return (links, booksWithMetaData.MetaData); //linkResponse: links, metaData: booksWithMetaData.MetaData
		}

		public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
		{
			var books = await _manager.BookRepository.GetAllBooksAsync(trackChanges);
			return books;
		}

		public async Task<IEnumerable<Book>> GetAllBooksWithDetails(bool trackChanges)
		{
			return await _manager.BookRepository.GetAllBooksWithDetailsAsync(trackChanges);
		}

		public async Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
		{
			var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

			return _mapper.Map<BookDto>(book); //veritabanından(book) BookDto turunde bir verinin donmesi saglandi
		}

		public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
		{
			var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

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
			var book = await GetOneBookByIdAndCheckExists(id, trackChanges);

			_mapper.Map(bookDto, book);

			//entity = _mapper.Map<Book>(bookDto);

			//_manager.BookRepository.Update(entity); 
			//bu satir olacaksa trackchanges false olmali, izlenen nesne degisiklerden sonra Update() olmadan da dogrudan save edilebilir
			//ya da update() kullanilacaksa degisiklikleri izlemeye gerek yok(trackChanges = false), kullanilmayacaksa degisiklikler izlenmeli

			//entity.Title = bookDto.Title;
			//entity.Price = bookDto.Price;
			await _manager.SaveAsync();
		}
		private async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
		{
			var book = await _manager.BookRepository.GetOneBookByIdAsync(id, trackChanges);
			if (book is null)
				throw new BookNotFoundException(id);
			return book;
		}
	}
}
