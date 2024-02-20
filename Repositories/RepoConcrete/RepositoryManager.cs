using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.RepoConcrete;
using Repositories.RepoContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoConcrete
{
	//unit of work pattern
	public class RepositoryManager : IRepositoryManager
	{
		private readonly RepositoryContext _context;
		private readonly IBookRepository _bookRepository;
		public RepositoryManager(RepositoryContext context, IBookRepository bookRepository)
		{
			_context = context;
			_bookRepository = bookRepository; 
		}
		public IBookRepository BookRepository => _bookRepository;
		public void Save()
		{
			_context.SaveChanges();
		}
	}
}

//eger alttaki gibi newleme islemi yapsaydik injection islemi yapmazdik ve IOC kaydina her bir repository icin gerek kalmazdi 
//public IBookRepository BookRepository => new BookRepository(_context); (_bookRepository yerine newleme) ama bu sıkı bagimliliktir 
//services.AddScoped<IBookRepository, BookRepository>(); bunun gibi bircok repository icin ioc kaydi yapacagiz eger DI kullanirsak.

