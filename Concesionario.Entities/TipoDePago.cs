using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class TipoDePago
	{
        public TipoDePago()
        {
            Ventas = new HashSet<Venta>();
        }
        public int TipoDePagoId { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
        public virtual ICollection<Venta> Ventas { get; set; }

    }
}
