using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch; //for [HttpPatch]
using Microsoft.AspNetCore.Mvc; //bir sınıfa controller olma ozelligini kazandırır
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/books")]
	public class BooksController : ControllerBase
	{
		private readonly IServiceManager _manager;
		public BooksController(IServiceManager manager)
		{
			_manager = manager;
		}
		[HttpGet]
		public IActionResult GetAllBooks()
		{
			var books = _manager.BookService.GetAllBooks(false);
			return Ok(books);
		}
		[HttpGet("{id:int}")]
		public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
		{
			var book = _manager
			.BookService
			.GetOneBookById(id, false);

			return Ok(book);
		}

		[HttpPost]
		public IActionResult CreateOneBook([FromBody] Book book)
		{
			if (book is null)
				return BadRequest(); // 400 

			_manager.BookService.CreateOneBook(book);

			return StatusCode(201, book);
		}

		[HttpPut("{id:int}")]
		public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
		{
			if (bookDto is null)
				return BadRequest();

			_manager.BookService.UpdateOneBook(id, bookDto, true);//degisiklikler izlenmeyecekse (false) service.cs'te _manager.BookRepository.Update(entity); yazılmali
			return Ok(bookDto);
		}

		[HttpDelete("{id:int}")]
		public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
		{
			_manager.BookService.DeleteOneBook(id, false);
			return NoContent();
		}
		[HttpPatch("{id:int}")]
		public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
		{
			// check entity
			var entity = _manager.BookService.GetOneBookById(id, true);

			bookPatch.ApplyTo(entity);
			_manager.BookService.UpdateOneBook(id, new BookDtoForUpdate(entity.Id, entity.Title, entity.Price), true); //bu satir gereksiz olmasa da oluyor

			return NoContent(); // 204
		}
	}
}
