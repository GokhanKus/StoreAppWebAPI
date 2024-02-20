using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.Context;
using Repositories.RepoContracts;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IRepositoryManager _manager;
		public BooksController(IRepositoryManager manager)
		{
			_manager = manager;
		}
		[HttpGet]
		public IActionResult GetAllBooks()
		{
			try
			{
				var books = _manager.BookRepository.GetAllBooks(false);
				return Ok(books);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpGet("{id:int}")]
		public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
		{
			try
			{
				var book = _manager
				.BookRepository
				.GetOneBookById(id, false);

				if (book is null)
					return NotFound(); //404

				return Ok(book);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		
		[HttpPost]
		public IActionResult CreateOneBook([FromBody] Book book)
		{
			try
			{
				if (book is null)
					return BadRequest(); // 400 

				_manager.BookRepository.CreateOneBook(book);
				_manager.Save();

				return StatusCode(201, book);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id:int}")]
		public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id,[FromBody] Book book)
		{
			try
			{
				// check book?
				var entity = _manager
					.BookRepository
					.GetOneBookById(id, true);

				if (entity is null)
					return NotFound(); // 404

				// check id
				if (id != book.Id)
					return BadRequest(); // 400

				entity.Title = book.Title;
				entity.Price = book.Price;

				_manager.Save();

				return Ok(book);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[HttpDelete("{id:int}")]
		public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
		{
			try
			{
				var entity = _manager
				 .BookRepository
				 .GetOneBookById(id, false);

				if (entity is null)
					return NotFound(new
					{
						statusCode = 404,
						message = $"Book with id:{id} could not found."
					});  // 404

				_manager.BookRepository.DeleteOneBook(entity);
				_manager.Save();

				return NoContent();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		[HttpPatch("{id:int}")]
		public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,[FromBody] JsonPatchDocument<Book> bookPatch)
		{
			try
			{
				// check entity
				var entity = _manager
					.BookRepository
					.GetOneBookById(id, true);

				if (entity is null)
					return NotFound(); // 404

				bookPatch.ApplyTo(entity);
				//_manager.BookRepository.UpdateOneBook(entity); bu satir gereksiz olmasa da oluyor
				_manager.Save();

				return NoContent(); // 204
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
