using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Pais
{
	public class PaisRequestDto
	{
		public int Id { get; set; }
		[StringLength(150)]
		public string Nombre { get; set; } = string.Empty;
	}
}
