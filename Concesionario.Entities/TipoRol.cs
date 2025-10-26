using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class TipoRol:IEntidad
	{
		public TipoRol()
		{
			Empleados = new HashSet<Empleado>();
		}
		public int Id { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
		public virtual ICollection<Empleado> Empleados { get; set; }
	}
}
