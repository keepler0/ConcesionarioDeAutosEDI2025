using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class TipoDePago
	{
		public int TipoDePagoId { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;

	}
}
