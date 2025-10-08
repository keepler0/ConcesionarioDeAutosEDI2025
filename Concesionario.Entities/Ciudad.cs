using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionario.Entities
{
	public class Ciudad:IEntidad
	{
        public Ciudad()
        {
            Clientes=new HashSet<Cliente>();
            Empleados = new HashSet<Empleado>();
        }
        public int Id { get; set; }
        [StringLength(150)]
		public string Nombre { get; set; } = string.Empty;
        public int CodigoPostal { get; set; }
        [ForeignKey(nameof(Provincia))]
        public int ProvinciaId { get; set; }
        public virtual Provincia? Provincia { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<Empleado> Empleados { get; set; }
    }
}
