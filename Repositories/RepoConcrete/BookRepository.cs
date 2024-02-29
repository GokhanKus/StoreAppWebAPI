﻿using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Extensions;
using Repositories.RepoContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoConcrete
{
	public class BookRepository : RepositoryBase<Book>, IBookRepository
	{
		public BookRepository(RepositoryContext context) : base(context)
		{

		}
		public void CreateOneBook(Book book) => Create(book);
		public void UpdateOneBook(Book book) => Update(book);
		public void DeleteOneBook(Book book) => Delete(book);
		public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
		{
			var books = await FindByCondition(b =>
			((b.Price >= bookParameters.MinPrice) &&
			(b.Price <= bookParameters.MaxPrice)), trackChanges)
				.OrderBy(i => i.Id)
				.ToListAsync();

			return PagedList<Book>.ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);
		}
		public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges)
		{
			return await FindByCondition(b => b.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
		}
	}
}
