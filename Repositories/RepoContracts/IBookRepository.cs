using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoContracts
{
	public interface IBookRepository:IRepositoryBase<Book>
	{
		Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
		Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
		void CreateOneBook(Book book);
		void UpdateOneBook(Book book);
		void DeleteOneBook(Book book);
	}
}
/*
CreateOneBook, UpdateOneBook, DeleteOneBook bu metotlarda asenkron bir iş yapmayacagiz, cunku o metotlarda tracking bazli bir yapı var nesneyi izliyor(save ederken async yapalim)
*/