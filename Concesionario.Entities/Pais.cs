using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Pais:IEntidad
	{
        public Pais()
        {
			Provincias = new HashSet<Provincia>();
			Autos = new HashSet<Auto>();
        }
		public int Id { get; set; }
		[StringLength(150)]
		public string Nombre { get; set; } = string.Empty;
        public virtual ICollection<Provincia>? Provincias { get; set; }
        public virtual ICollection<Auto> Autos { get; set; }
    }
}
