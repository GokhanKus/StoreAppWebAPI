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

		private User? _user;
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

		public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
		{
			_user = await _userManager.FindByNameAsync(userForAuthDto.UserName);
			var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password));
			if (!result)
				_logger.LogWarning($"{nameof(ValidateUser)} : Authentication failed. Wrong username or password.");

			return result;
		}
	}
}
