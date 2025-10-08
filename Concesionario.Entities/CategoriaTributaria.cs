using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Concesionario.Entities
{
	public class CategoriaTributaria:IEntidad
	{
        public CategoriaTributaria()
        {
            Clientes=new HashSet<Cliente>();
        }
        public int Id { get; set; }
		[StringLength(50)]
        public string Descripcion { get; set; } = string.Empty;
        public ICollection<Cliente> Clientes { get; set; }
    }
}
