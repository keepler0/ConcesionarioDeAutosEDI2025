using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Carroceria
{
	public class CarroceriaRequestDto
	{
		public int Id { get; set; }
		[StringLength(100)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
