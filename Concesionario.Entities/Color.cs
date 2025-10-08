using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Color:IEntidad,ICaracteristicaAuto<Auto>
	{
        public Color()
        {
            Autos=new HashSet<Auto>();
        }
        public int Id { get; set; }
		[StringLength(150)]
		public string Descripcion { get; set; } = string.Empty;
        public virtual ICollection<Auto> Autos { get; set; }
    }
}
