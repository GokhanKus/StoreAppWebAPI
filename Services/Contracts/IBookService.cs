using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IBookService
	{
		Task<(IEnumerable<BookDto>, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
		Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges);
		Task<BookDto> CreateOneBookAsync(BookDtoForInsertion book);
		Task DeleteOneBookAsync(int id, bool trackChanges);
		Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges);
		Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges); //tuple, geriye donus yapacagim tipler:BookDtoForUpdate ve Book
																												 //Tuple<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatch(int id, bool trackChanges); ustekiyle ayni yazim
		Task SaveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book);
	}
}
/*
repo interfacesinde create delete update'leri Task async yapmadik cunku orada saveasync yoktu orada tracking oldugu icin degisiklikler izleniyordu 
service katmanında saveasync calistigi icin burada create update delete metotlarını da task ile sarmalliyoruz
 */