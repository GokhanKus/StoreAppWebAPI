using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch; //for [HttpPatch]
using Microsoft.AspNetCore.Mvc; //bir sınıfa controller olma ozelligini kazandırır
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[ServiceFilter(typeof(LogFilterAttribute))] //calistirdigimiz butun actionlar loga dusecek => StoreAppWebAPI\WebApi\bin\Debug\net8.0\logs\2024-02-27_logfile.txt
	[ApiController]
	[Route("api/books")]
	public class BooksController : ControllerBase
	{
		private readonly IServiceManager _manager;
		public BooksController(IServiceManager manager)
		{
			_manager = manager;
		}

		[ServiceFilter(typeof(ValidateMediaTypeAttribute))]
		[HttpGet]
		public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookParameters bookParameters) //books?pageNumber=2&pageSize=10
		{
			//FromQuery diyerek bu ifadenin query string oldugunu queryden gelecegini belirtelim
			var pagedResult = await _manager.BookService.GetAllBooksAsync(bookParameters, false);
			Response.Headers["X-Pagination"] = JsonSerializer.Serialize(pagedResult.metaData);
			#region Response.Headers["X-Pagination"]
			/*
			Bu kod parçası, bir HTTP yanıtının başlık bölümüne "X-Pagination" adında özel bir başlık ekler.Bu başlık, sayfalama işlemiyle ilgili ek bilgileri taşır.
			Örneğin, bir web uygulaması üzerinden birçok sonuç getiriyorsanız ve bunları sayfalara ayırıyorsanız,
			her bir sayfa için kaç tane öğe olduğunu, toplam öğe sayısını, mevcut sayfa numarasını ve benzeri bilgileri bu "X-Pagination" başlığı içinde gönderebilirsiniz.
			Yani, Response.Headers["X-Pagination"] ifadesi, HTTP yanıtının başlık bölümünde bir başlık eklerken, 
			bu başlık içine yerleştirilecek veri, sayfalama işlemiyle ilgili bilgileri içerir.Bu bilgiler JSON formatına dönüştürülerek başlığa eklenir,
			böylece istemciye(tarayıcıya veya diğer istemcilere) sunulan verilerin nasıl sayfalara ayrıldığını anlamaları için ek bilgi sağlanmış olur.
			Response Header'da, örnegin boyle bir sorgu atarsak: "/api/Books?pageSize=20&pageNumber=2", asagidaki gibi cikti aliriz;
			X-Pagination = {"CurrentPage":2,"TotalPage":5,"PageSize":20,"TotalCount":91,"HasPreviousPage":true,"HasNextPage":true}
			*/
			#endregion

			return Ok(pagedResult.books);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
		{
			var book = await _manager
			.BookService
			.GetOneBookByIdAsync(id, false);

			return Ok(book);
		}
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[HttpPost]
		public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
		{
			#region ValidationFilterAttribute sayesinde null veya modelstate.IsValid check yapmamiza gerek kalmadi

			//if (bookDto is null)
			//	return BadRequest(); // 400 

			//if (!ModelState.IsValid) //model gecerli degilse 422 ile donelim
			//	return UnprocessableEntity(ModelState); //program.cs'te SuppressModelStateInvalidFilter = true bu kısmı yazdiktan sonra bu if kontrolunu yapmazsak invalid olsa bile ekler
			#endregion
			var book = await _manager.BookService.CreateOneBookAsync(bookDto);
			return StatusCode(201, book);
		}

		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
		{
			#region ValidationFilterAttribute sayesinde null veya modelstate.IsValid check yapmamiza gerek kalmadi

			//if (bookDto is null)
			//	return BadRequest();

			//if (!ModelState.IsValid)
			//	return UnprocessableEntity(ModelState);
			#endregion
			await _manager.BookService.UpdateOneBookAsync(id, bookDto, true);//degisiklikler izlenmeyecekse (false) service.cs'te _manager.BookRepository.Update(entity); yazılmali
			return Ok(bookDto);
		}

		[HttpPatch("{id:int}")]
		public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDtoForUpdate> bookPatch)
		{
			if (bookPatch is null)
				return BadRequest();

			var result = await _manager.BookService.GetOneBookForPatchAsync(id, true);
			bookPatch.ApplyTo(result.bookDtoForUpdate, ModelState);

			TryValidateModel(result.bookDtoForUpdate);

			if (!ModelState.IsValid)
				return UnprocessableEntity(ModelState);

			await _manager.BookService.SaveChangesForPatchAsync(result.bookDtoForUpdate, result.book);
			return NoContent(); // 204
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteOneBookAsync([FromRoute(Name = "id")] int id)
		{
			await _manager.BookService.DeleteOneBookAsync(id, false);
			return NoContent();
		}
	}
}
