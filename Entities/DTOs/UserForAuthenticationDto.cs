using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
	//login olunurken user name ve passwordunu user'dan almak icin ve user'i dogrulamak(validate) icin bu classi olusturduk
	public record UserForAuthenticationDto
	{
		[Required(ErrorMessage = "Username is required")]
		public string? UserName{ get; init; }

		[Required(ErrorMessage = "Password is required")]
		public string? Password{ get; init; }
    }
}
