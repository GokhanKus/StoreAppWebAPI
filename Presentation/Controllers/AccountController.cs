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

			var tokenDto = await _services.AuthService.CreateToken(populateExpiry: true);//token sonlanma suresi true ise tokenin suresi doldu demektir.
			return Ok(tokenDto);

			//[Authorize] olan GetAllBooksAsync metoduna erisebilmek icin uretilen token ile postman'de authorization kısmında bearer token secerek erisilebilir
			//Postmande Books'ta Autherization kismina Tokeni ver, crud islemlerinde(booksconroller actionlarında) autherization kismina type: Inherit auth from parent
			//Postman icin access token olusturuldu,Postmande Books'ta Autherization kismina Tokeni {{accessToken}}degiskenine aldik ve
			//Account / Authenticate (Generate JWT)'de tests kismindaki kod.

			//Refresh Token
			//kurumsal firmalarda genellikle tokenlerin suresi 30 dk, 1 saat değil de 5 dk gibi kısa süreler de olur, cunku encode edilen bu tokenler decoder ile basit bir sekilde cozulecegi icin ve
			//kotu niyetli kisilerin bu tokeni alıp kullanabilecegi icin 5 dk gibi kısa sureli tokenler olusturulur ve refresh edilir refresh token bu yuzden kullanilir
		}
	}
}
