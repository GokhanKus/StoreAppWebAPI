using Microsoft.EntityFrameworkCore.Metadata;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
	public class ServiceManager : IServiceManager
	{
		private readonly IBookService _bookService;
		private readonly IAuthService _authService;
		public ServiceManager(IBookService bookService, IAuthService authService)
		{
			_bookService = bookService;
			_authService = authService;
		}
		public IBookService BookService => _bookService;
		public IAuthService AuthService => _authService;
	}
}
