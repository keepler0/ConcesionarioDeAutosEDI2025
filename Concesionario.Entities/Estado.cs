using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Estado:IEntidad,ICaracteristicaAuto<Auto>
	{
        public Estado()
        {
            Autos=new HashSet<Auto>();
        }
        public int Id { get; set; }
        [StringLength(12)]
        public string Descripcion { get; set; } = string.Empty;
		public ICollection<Auto> Autos { get; set; }
	}
}
