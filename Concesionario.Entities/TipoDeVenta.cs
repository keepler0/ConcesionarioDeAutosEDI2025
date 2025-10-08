using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class TipoDeVenta:IEntidad
	{
		public int Id { get; set; }
		[StringLength(150)]
		public string Descripcion { get; set; } = string.Empty;
        //public ICollection<Venta> Ventas { get; set; } realizar para la relacion entre ventas y tipos de ventas
    }
}
