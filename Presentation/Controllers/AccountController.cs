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
	}
}
