using Concesionario.Abstractions;

namespace Concesionario.Entities
{
	public class Rol:IEntidad
	{
		public Rol()
		{
			Empleados = new HashSet<Empleado>();
		}
		public int Id { get; set; }
		public string Descripcion { get; set; } = string.Empty;
		public ICollection<Empleado> Empleados { get; set; }
	}
}
