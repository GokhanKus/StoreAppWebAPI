using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Extensions;
using Repositories.RepoContracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoConcrete
{
	//sealed: bu classin bir daha devralinamayacagini belirtiri bu classin son versiyonudur inherit edilmesi artik mumkun degildir.
	public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
	{
		public BookRepository(RepositoryContext context) : base(context)
		{

		}
		public void CreateOneBook(Book book) => Create(book);
		public void UpdateOneBook(Book book) => Update(book);
		public void DeleteOneBook(Book book) => Delete(book);
		public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
		{
			var books = await FindAll(trackChanges)
				.FilterBooksWithName(bookParameters.SearchingTerm)
				.FilterBooksWithPrice(bookParameters.MinPrice, bookParameters.MaxPrice)
				.Sort(bookParameters.OrderBy)
				.ToListAsync();

			return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
		}
		public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges)
		{
			return await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
		}
		public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
		{
			return await FindAll(trackChanges).OrderBy(b => b.Id).ToListAsync();
			//return await _context.Books.ToListAsync();
			//ilk ifade, DbSet üzerinde bir sorgu oluştururken ikinci ifade, veritabanına gitmek ve verileri doğrudan almak için bir sorgu yürütür.
			//İlk ifade, daha fazla esneklik sağlayabilir çünkü DbSet'in üzerinde sorgular yapılabilirken, ikinci ifade daha doğrudan bir yaklaşım sunar. 
		}

		public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackChanges)
		{
			//return await FindAll(trackChanges).Include(b=>b.Category).ToListAsync();
			return await _context.Books.Include(b => b.Category).OrderBy(b => b.Id).ToListAsync();
		}
	}
}
