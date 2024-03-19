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
		private readonly ICategoryService _categoryService;
		private readonly IAuthService _authService;
		public ServiceManager(IBookService bookService, IAuthService authService, ICategoryService categoryService)
		{
			_bookService = bookService;
			_authService = authService;
			_categoryService = categoryService;
		}
		public IBookService BookService => _bookService;
		public ICategoryService CategoryService => _categoryService;
		public IAuthService AuthService => _authService;
	}
}
