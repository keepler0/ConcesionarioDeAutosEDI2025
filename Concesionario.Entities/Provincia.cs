using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionario.Entities
{
	public class Provincia:IEntidad
	{
		public Provincia()
		{
			Ciudades = new HashSet<Ciudad>();
		}
		public int Id { get; set; }
		[StringLength(150)]
		public string Nombre { get; set; } = string.Empty;
		[ForeignKey(nameof(Pais))]
		public int PaisId { get; set; }
		public virtual Pais? Pais { get; set; }
		public virtual ICollection<Ciudad> Ciudades { get; set; }
	}
}
