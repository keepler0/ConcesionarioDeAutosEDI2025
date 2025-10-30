using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Identity.User
{
	public class UserRegistroRequestDto
	{
		[Required]
		public string Nombres { get; set; } = string.Empty;
		[Required]
		public string Apellidos { get; set; } = string.Empty;
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; }
		//public required string Password { get; set; }
		[DataType(DataType.Date)]
		public DateTime? FechaNacimiento { get; set; }
	}
}
