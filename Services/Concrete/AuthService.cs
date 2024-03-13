using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly ILoggerService _logger;
		private readonly IConfiguration _configuration;

		private User? _user;
		public AuthService(IMapper mapper, UserManager<User> userManager, ILoggerService logger, IConfiguration configuration)
		{
			_userManager = userManager;
			_mapper = mapper;
			_logger = logger;
			_configuration = configuration;
		}

		public async Task<string> CreateToken()
		{
			var signinCredentials = GetSignInCredentials(); //kimlik bilgileri alindi
			var claims = await GetClaims();                 //claimsler alindi (rol, hak, iddia)
			var tokenOptions = GenerateTokenOptions(signinCredentials, claims);//token olusturma secenekleri generate edildi
			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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
		private SigningCredentials GetSignInCredentials()
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
			var secret = new SymmetricSecurityKey(key);
			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}
		private async Task<List<Claim>> GetClaims()
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name,_user.UserName)
			};
			var roles = await _userManager.GetRolesAsync(_user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			return claims;
		}
		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var tokenOptions = new JwtSecurityToken(

				issuer: jwtSettings["validIssuer"],
				audience: jwtSettings["validAudience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
				signingCredentials: signinCredentials);
			
			return tokenOptions;
		}
	}
}
