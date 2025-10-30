using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Transmision
{
	public class TransmisionRequestDto
	{
		public int Id { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
