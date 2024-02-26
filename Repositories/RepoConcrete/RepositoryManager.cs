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
		//private readonly Lazy<IBookRepository> _bookRepository;
		#region Lazy loading & Eager loading
		/*
		eager loading tek seferde iliskili verileri(relationship)birincil nesne(örn kitap)ile birlikte butun verileri(kitabın yazari) ihtiyac duyulmaksizin onceden yukler ve tum dataları dbden alir,
		join islemi kullanir, tum dataların birlesmis halde alınmasini saglar, ihtiyac olmamasina ragmen getirdigi icin performans duser ve bellek kullanimini artirir

		lazy loading iliskili nesneleri, birincil nesne ile beraber yuklemez, bir nesneye ihtiyac duyuldugu anda yuklenecegi anlamına gelir
		iliskili verilere erisildiginde veya cagrildiginda ek sorgular yaparak veri yuklenir ve ek sorgular yapildigi icin performansi olumsuz etkileyebilir
		ancak sadece ihtiyac duyuldugu anda verileri getireceginden performans artisi ve bellek kullanimini da azaltabilir.

		ozetle eager loading tek seferde butun veriyi cekerken, lazy loading ihtiyac duyuldugu anda getirir ancak her seferinde sorgu atar(n+1 problem)
		cok fazla sayida kaydimiz var ise 5000, 10000 gibi
		eager loading hepsini iliskisel verilerle beraber tek seferde yuklerken
		lazy loading her biri icin sorgu atacaktır ve eagera gore daha maliyetli olacaktır
		 */
		#endregion
		public RepositoryManager(RepositoryContext context, IBookRepository bookRepository)
		{
			_context = context;
			_bookRepository = bookRepository;
			/*new Lazy<IBookRepository>(() => new BookRepository(_context));*/
		}
		public IBookRepository BookRepository => _bookRepository;
		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}

//eger alttaki gibi newleme islemi yapsaydik injection islemi yapmazdik ve IOC kaydina her bir repository icin gerek kalmazdi 
//public IBookRepository BookRepository => new BookRepository(_context); (_bookRepository yerine newleme) ama bu sıkı bagimliliktir 
//services.AddScoped<IBookRepository, BookRepository>(); bunun gibi bircok repository icin ioc kaydi yapacagiz eger DI kullanirsak.

