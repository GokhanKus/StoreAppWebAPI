using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IBookService
	{
		IEnumerable<BookDto> GetAllBooks(bool trackChanges);
		BookDto GetOneBookById(int id, bool trackChanges);
		BookDto CreateOneBook(BookDtoForInsertion book);
		void DeleteOneBook(int id, bool trackChanges);
		void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges);
		(BookDtoForUpdate bookDtoForUpdate, Book book) GetOneBookForPatch(int id, bool trackChanges); //tuple, geriye donus yapacagim tipler:BookDtoForUpdate ve Book
		//Tuple<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatch(int id, bool trackChanges); ustekiyle ayni yazim
		void SaveChangesForPatch(BookDtoForUpdate bookDtoForUpdate, Book book);
	}
}
