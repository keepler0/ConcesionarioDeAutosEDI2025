using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Marca:IEntidad,ICaracteristicaAuto<Auto>
	{
        public Marca()
        {
            Autos=new HashSet<Auto>();
        }
        public int Id { get; set; }
		[StringLength(150)]
		public string Descripcion { get; set; } = string.Empty;
		public ICollection<Auto> Autos { get; set; }
	}
}
