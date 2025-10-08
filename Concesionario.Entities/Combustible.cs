using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Combustible:IEntidad,ICaracteristicaAuto<Auto>
	{
        public Combustible()
        {
            Autos=new HashSet<Auto>();
        }
        public int Id { get; set; }
		[StringLength(6)]
		public string Descripcion { get; set; } = string.Empty;
        public virtual ICollection<Auto> Autos { get; set; }
    }
}
