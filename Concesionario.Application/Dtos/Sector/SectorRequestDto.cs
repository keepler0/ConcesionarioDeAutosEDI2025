using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Sector
{
	public class SectorRequestDto
	{
		public int Id { get; set; }
		[StringLength(50)]
		public string Descripcion { get; set; } = string.Empty;
	}
}
