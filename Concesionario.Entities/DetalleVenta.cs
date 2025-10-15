using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Concesionario.Entities
{
	public class DetalleVenta : IEntidad
	{
		public int Id { get; set; }
		[ForeignKey(nameof(Venta))]
		public int VentaId { get; set; }
        public virtual Venta? Venta { get; set; }
		[ForeignKey(nameof(Auto))]
        public int AutoId { get; set; }
        public virtual Auto? Auto { get; set; }
		[DataType(DataType.Currency)]
        public decimal PrecioUnitario { get; set; }
	}
}
