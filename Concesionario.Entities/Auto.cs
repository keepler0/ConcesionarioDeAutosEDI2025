using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Concesionario.Entities
{
	public class Auto:IEntidad
	{
        public Auto()
        {
			DetallesVentas = new HashSet<DetalleVenta>();
        }
        public int Id { get; set; }
		[StringLength(50)]
		public string Modelo { get; set; } = string.Empty;
		[StringLength(20)]
		public string Version { get; set; } = string.Empty;
		public int Anio { get; set; }
		public int Kilometros { get; set; }
		[StringLength(7)]
		public string Matricula { get; set; } = string.Empty;
		[StringLength(50)]
		public string Motor { get; set; } = string.Empty;
		[StringLength(30)]
		//[Index(IsUnique=true)]
		public string NumeroMotor { get; set; } = string.Empty;
		[StringLength(17)]
		public string NumeroChasis { get; set; } = string.Empty;
		public int CantAsientos { get; set; }
		[DataType(DataType.Currency)]
		public decimal Precio { get; set; }
		
		[ForeignKey(nameof(Marca))]
		public int MarcaId { get; set; }
		public virtual Marca? Marca { get; set; }
		
		[ForeignKey(nameof(Carroceria))]
		public int CarroceriaId { get; set; }
		public virtual Carroceria? Carroceria { get; set; }
		
		[ForeignKey(nameof(Color))]
		public int ColorId { get; set; }
		public virtual Color? Color { get; set; }
		
		[ForeignKey(nameof(Combustible))]
		public int CombustibleId { get; set; }
		public virtual Combustible? Combustible { get; set; }
		
		[ForeignKey(nameof(Estado))]
		public int EstadoId { get; set; }
		public virtual Estado? Estado { get; set; }
		
		[ForeignKey(nameof(Pais))]
		public int PaisId { get; set; }
		public virtual Pais? Pais { get; set; }
		
		[ForeignKey(nameof(Traccion))]
		public int TraccionId { get; set; }
		public virtual Traccion? Traccion { get; set; }

		[ForeignKey(nameof(Transmision))]
		public int TransmisionId { get; set; }
		public virtual Transmision? Transmision { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVentas { get; set; }
    }
}
