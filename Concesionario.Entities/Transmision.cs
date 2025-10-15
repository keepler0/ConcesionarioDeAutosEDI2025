using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Transmision:IEntidad,ICaracteristicaAuto<Auto>
	{
        public Transmision()
        {
            Autos=new HashSet<Auto>();
        }
        public int Id { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
		public virtual ICollection<Auto> Autos { get; set; }
	}
}
