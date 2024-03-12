using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly ILoggerService _logger;
		//private readonly IConfiguration _configuration;
		public AuthService(IMapper mapper, UserManager<User> userManager, ILoggerService logger)
		{
			_userManager = userManager;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<IdentityResult> DeleteUserByEmail(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user == null)
				throw new Exception("no users found");

			var result = await _userManager.DeleteAsync(user);
			return result;

		}

		public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
		{
			var user = _mapper.Map<User>(userForRegistrationDto);

			var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

			if (result.Succeeded)
				await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);

			return result;
		}
	}
}
