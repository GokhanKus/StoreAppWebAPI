using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[ApiVersion("2.0",Deprecated = true)] //bu versiyon deprecated edildi demis oluyoruz yani kullanimdan kaldirildigini artik supported olmadigi bilgisini client ile paylasiyoruz.
	[ApiController]
	[Route("api/books")]// => localhost:46515/api/v2.0/books
	//[Route("api/v{version:apiVersion}/books")]// => localhost:46515/api/v2.0/books
	public class BooksV2Controller : ControllerBase
	{
		private readonly IServiceManager _manager;
		public BooksV2Controller(IServiceManager manager)
		{
			_manager = manager;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllBooksAsync()
		{
			var books = await _manager.BookService.GetAllBooksAsync(false);
			var booksV2 = books.Select(b => new { Id = b.Id, Title = b.Title });
			return Ok(booksV2);
		}
		//query string ile versiyonlama, {{baseUrl}}/api/books?api-version=2.0
		//url ile versiyonlama yaptik,	{{baseUrl}}/api/v2.0/books
		//simdi de header ile versiyonlama yapalim
	}
}
