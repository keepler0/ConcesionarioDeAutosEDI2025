using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Combustible
{
	public class CombustibleRequestDto
	{
		public int Id { get; set; }
		[StringLength(6)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
