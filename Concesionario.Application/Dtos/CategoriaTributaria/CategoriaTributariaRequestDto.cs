using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.CategoriaTributaria
{
	public class CategoriaTributariaRequestDto
	{
        public int Id { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; } = string.Empty;
    }
}
