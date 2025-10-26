using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Estado
{
	public class EstadoRequestDto
	{
		public int Id { get; set; }
		[StringLength(12)]
		public string Descripcion { get; set; } = string.Empty;

	}
}
