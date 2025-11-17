namespace Concesionario.Application.Dtos.Auto
{
	public class AutoResponseDto
	{
		public int id { get; set; }
		public string Marca { get; set; } = string.Empty;
		public string Modelo { get; set; } = string.Empty;
		public int Anio { get; set; }
		public string Version { get; set; } = string.Empty;
		public int Kilometros { get; set; }
		public string Motor { get; set; } = string.Empty;
		public int CantAsientos { get; set; }
		public decimal Precio { get; set; }
		public string Carroceria { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;
		public string Combustible { get; set; } = string.Empty;
		public string Estado { get; set; } = string.Empty;
		public string Pais { get; set; } = string.Empty;
		public string Traccion { get; set; } = string.Empty;
		public string Transmision { get; set; } = string.Empty;
	}
}
