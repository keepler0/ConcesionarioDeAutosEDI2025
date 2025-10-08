using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Sector:IEntidad
	{
        public Sector()
        {
            Empleados=new HashSet<Empleado>();
        }
        public int Id { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
        public ICollection<Empleado> Empleados { get; set; }
    }
}
