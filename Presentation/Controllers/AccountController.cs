using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
	[Route("api/auth")]
	public class AccountController : ControllerBase
	{
		private readonly IServiceManager _services;

		public AccountController(IServiceManager services)
		{
			_services = services;
		}

		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
		{
			var result = await _services.AuthService.RegisterUser(userForRegistrationDto);
			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
				{
					ModelState.TryAddModelError(err.Code, err.Description);
				}
				return BadRequest(ModelState);
			}
			return StatusCode(201); //return Created();
		}

		[HttpDelete("{email}")]
		public async Task<IActionResult> DeleteUser([FromRoute(Name = "email")] string email)
		{
			var result = await _services.AuthService.DeleteUserByEmail(email);
			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
				{
					ModelState.TryAddModelError(err.Code, err.Description);
				}
				return BadRequest(ModelState);
			}
			return StatusCode(201); //return Created();

		}

		[HttpPost("login")]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthDto)
		{
			if (!await _services.AuthService.ValidateUser(userForAuthDto))
				return Unauthorized(); //401

			return Ok(new
			{
				Token = await _services.AuthService.CreateToken()
			});
			//[Authorize] olan GetAllBooksAsync metoduna erisebilmek icin uretilen token ile postman'de authorization kısmında bearer token secerek erisilebilir
		}
	}
}
