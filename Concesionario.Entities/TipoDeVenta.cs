using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class TipoDeVenta:IEntidad
	{
        public TipoDeVenta()
        {
            Ventas = new HashSet<Venta>();
        }
        public int Id { get; set; }
		[StringLength(150)]
		public string Descripcion { get; set; } = string.Empty;
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
