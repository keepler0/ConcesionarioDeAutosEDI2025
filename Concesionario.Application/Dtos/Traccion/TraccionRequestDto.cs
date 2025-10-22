using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Traccion
{
	public class TraccionRequestDto
	{
		public int Id { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
