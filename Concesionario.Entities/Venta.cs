using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionario.Entities
{
	public class Venta : IEntidad
	{
        public Venta()
        {
            DetallesVentas = new HashSet<DetalleVenta>();
        }
        public int Id { get; set; }
		[ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }
        [ForeignKey(nameof(Empleado))]
        public int EmpleadoId { get; set; }
        public virtual Empleado? Empleado { get; set; }
        [ForeignKey(nameof(TipoDeVenta))]
        public int TipoDeVentaId { get; set; }
        public virtual TipoDeVenta? TipoDeVenta { get; set; }
        [ForeignKey(nameof(TipoDePago))]
        public int TipoDePagoId { get; set; }
        public virtual TipoDePago? TipoDePago { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }
        public ICollection<DetalleVenta> DetallesVentas { get; set; }
    }
}
