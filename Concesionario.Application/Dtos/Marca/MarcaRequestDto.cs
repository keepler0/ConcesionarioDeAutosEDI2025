using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Marca
{
	public class MarcaRequestDto
	{
		public int Id { get; set; }
		[StringLength(150)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
