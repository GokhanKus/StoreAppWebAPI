using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch; //for [HttpPatch]
using Microsoft.AspNetCore.Mvc; //bir sınıfa controller olma ozelligini kazandırır
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
		public IActionResult CreateOneBook([FromBody] BookDtoForInsertion bookDto)
		{
			if (bookDto is null)
				return BadRequest(); // 400 

			if (!ModelState.IsValid) //model gecerli degilse 422 ile donelim
				return UnprocessableEntity(ModelState); //program.cs'te SuppressModelStateInvalidFilter = true bu kısmı yazdiktan sonra bu if kontrolunu yapmazsak invalid olsa bile ekler

			_manager.BookService.CreateOneBook(bookDto);

			return StatusCode(201, bookDto);
		}

		[HttpPut("{id:int}")]
		public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
		{
			if (bookDto is null)
				return BadRequest();

			if (!ModelState.IsValid) 
				return UnprocessableEntity(ModelState);

			_manager.BookService.UpdateOneBook(id, bookDto, true);//degisiklikler izlenmeyecekse (false) service.cs'te _manager.BookRepository.Update(entity); yazılmali
			return Ok(bookDto);
		}

		[HttpPatch("{id:int}")]
		public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDto> bookPatch)
		{
			// check entity
			var bookDto = _manager.BookService.GetOneBookById(id, true);

			bookPatch.ApplyTo(bookDto);
			_manager.BookService.UpdateOneBook(id, new BookDtoForUpdate(bookDto.Id, bookDto.Title, bookDto.Price), true);

			return NoContent(); // 204
		}

		[HttpDelete("{id:int}")]
		public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
		{
			_manager.BookService.DeleteOneBook(id, false);
			return NoContent();
		}
	}
}
