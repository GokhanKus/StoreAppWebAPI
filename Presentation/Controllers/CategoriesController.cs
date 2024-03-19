using Asp.Versioning;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/categories")]// => localhost:46515/api/v2.0/books
							 //[ApiExplorerSettings(GroupName = "v1")],
	public class CategoriesController : ControllerBase
	{
		private readonly IServiceManager _manager;

		public CategoriesController(IServiceManager manager)
		{
			_manager = manager;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategoriesAsync()
		{
			var categories = await _manager.CategoryService.GetAllCategoriesAsync(false);
			return Ok(categories);
		}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetOneCategoryAsync([FromRoute(Name = "id")] int id)
		{
			var category = await _manager.CategoryService.GetOneCategoryByIdAsync(id, false);
			return Ok(category);
		}

		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[HttpPost(Name = "CreateOneCategoryAsync")]
		public async Task<IActionResult> CreateOneCategoryAsync([FromBody] CategoryDtoForInsertion categoryDto)
		{
			var category = await _manager.CategoryService.CreateOneCategoryAsync(categoryDto);
			return StatusCode(201, category);
		}

		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateOneCategoryAsync([FromRoute] int id, [FromBody] CategoryDtoForUpdate categoryDto)
		{
			await _manager.CategoryService.UpdateOneCategoryAsync(id, categoryDto, true);
			return Ok(categoryDto);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public async Task<IActionResult> DeleteOneCategoryAsync([FromRoute(Name = "id")] int id)
		{
			await _manager.CategoryService.DeleteOneCategoryAsync(id, false);
			return NoContent();
		}
	}
}
