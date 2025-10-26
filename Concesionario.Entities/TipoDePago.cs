using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class TipoDePago:IEntidad
	{
		public int Id { get; set; }
        public TipoDePago()
        {
            Ventas = new HashSet<Venta>();
        }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
