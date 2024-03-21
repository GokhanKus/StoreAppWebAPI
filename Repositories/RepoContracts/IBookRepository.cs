using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoContracts
{
	public interface IBookRepository : IRepositoryBase<Book>
	{
		Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
		Task<List<Book>> GetAllBooksAsync(bool trackChanges); //asiri yuklenebilen bir metot v2 bookscontroller icin daha yalin bir metot..
		Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
		void CreateOneBook(Book book);
		void UpdateOneBook(Book book);
		void DeleteOneBook(Book book);
		Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges);//kitap kategorilerini de dahil ediyoruz.
	}
}
/*
CreateOneBook, UpdateOneBook, DeleteOneBook bu metotlarda asenkron bir iş yapmayacagiz, cunku o metotlarda tracking bazli bir yapı var nesneyi izliyor(save ederken async yapalim)
*/