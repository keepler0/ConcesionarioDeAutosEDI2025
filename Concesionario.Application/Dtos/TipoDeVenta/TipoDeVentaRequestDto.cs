using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.TipoDeVenta
{
	public class TipoDeVentaRequestDto
	{
		public int Id { get; set; }
		[StringLength(150)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
