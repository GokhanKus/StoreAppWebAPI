using System.Runtime.Serialization;

namespace Entities.DTOs
{
	public record CategoryDtoForInsertion
	{
		public string? CategoryName { get; init; }
	}
}

