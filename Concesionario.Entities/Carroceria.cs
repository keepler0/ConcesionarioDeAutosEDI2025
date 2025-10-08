using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class Carroceria: IEntidad, ICaracteristicaAuto<Auto>
	{
        public Carroceria()
        {
            Autos=new HashSet<Auto>();
        }
        public int Id { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; } = string.Empty;
        public virtual ICollection<Auto> Autos { get; set; }
    }
}
