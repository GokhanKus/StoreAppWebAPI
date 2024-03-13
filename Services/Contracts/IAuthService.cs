using Entities.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IAuthService
	{
		Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
		Task<IdentityResult> DeleteUserByEmail(string email);
		Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto);
		Task<string> CreateToken();
	}
}
