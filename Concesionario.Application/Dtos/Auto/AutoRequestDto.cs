using System.ComponentModel.DataAnnotations;

namespace Concesionario.Application.Dtos.Auto
{
	public class AutoRequestDto
	{
		public int id { get; set; }
		public int MarcaId { get; set; }
		public string Modelo { get; set; } = string.Empty;

		public int Anio { get; set; }
		[StringLength(20)]
		public string Version { get; set; } = string.Empty;
		public int Kilometros { get; set; }
		[StringLength(7)]
		public string Matricula { get; set; } = string.Empty;
		[StringLength(50)]
		public string Motor { get; set; } = string.Empty;

		public string NumeroMotor { get; set; } = string.Empty;
		[StringLength(17)]
		public string NumeroChasis { get; set; } = string.Empty;
		public int CantAsientos { get; set; }
		[DataType(DataType.Currency)]
		public decimal Precio { get; set; }
		public int CarroceriaId { get; set; }
		public int ColorId { get; set; } 
		public int CombustibleId { get; set; } 
		public int EstadoId { get; set; }
		public int PaisId { get; set; }
		public int TraccionId { get; set; }
		public int TransmisionId { get; set; } 



	}
}
