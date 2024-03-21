using Asp.Versioning;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
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
	[Route("api/files")]
	public class FilesController : ControllerBase
	{
		[HttpPost("upload")]
		public async Task<IActionResult> Upload(IFormFile file) // => Upload/Post
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media"); //=>  "C:\\ASP.NET_Projects\\StoreAppWebAPI\\WebApi\\Media"

			if (!Directory.Exists(folder))          //yoksa olusturalim
				Directory.CreateDirectory(folder);

			var path = Path.Combine(folder, file.FileName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return Ok(new
			{
				file = file.FileName,
				path = path,
				size = file.Length
			});
		}
	}
}
