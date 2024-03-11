using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
	public record UserForRegistrationDto
	{
		public string? FirstName { get; init; }
		public string? LastName { get; init; }
		public string? PhoneNumber { get; init; }

		[Required]
		public string? UserName { get; init; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; init; }

		[Required]
		[DataType(DataType.Password)]
		public string? Password { get; init; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
		public string? ConfirmPassword { get; init; }

		public ICollection<string>? Roles { get; init; }
		//public HashSet<string>? Roles { get; set; } = new HashSet<string>();
	}
}
