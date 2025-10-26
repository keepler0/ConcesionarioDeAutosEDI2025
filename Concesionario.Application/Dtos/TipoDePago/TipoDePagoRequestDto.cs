using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.TipoDePago
{
	public class TipoDePagoRequestDto
	{
		public int Id { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
