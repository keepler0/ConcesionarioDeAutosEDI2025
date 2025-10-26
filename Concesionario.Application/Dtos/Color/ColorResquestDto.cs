using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Color
{
	public class ColorResquestDto
	{
		public int Id { get; set; }
		[StringLength(150)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
