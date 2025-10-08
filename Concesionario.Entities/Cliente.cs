using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionario.Entities
{
	public class Cliente : IEntidad
	{
		public int Id { get; set; }
		[StringLength(20)]
		public string PrimerNombre { get; set; } = string.Empty;
		[StringLength(20)]
		public string SegundoNombre { get; set; } = string.Empty;
		[StringLength(100)]
		public string Apellido { get; set; } = string.Empty;
		[StringLength(8)]
		public string Dni { get; set; } = string.Empty;
		[StringLength(13)]
		public string Cuit { get; set; } = string.Empty;
		[StringLength(13)]
		public string Cuil { get; set; } = string.Empty;
		[StringLength(10)]
		public string Telefono { get; set; } = string.Empty;
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = string.Empty;
		[StringLength(100)]
		public string Direccion { get; set; } = string.Empty;
		public int NumeroCasa { get; set; }
		[ForeignKey(nameof(CategoriaTributaria))]
		public int CategoriaTributariaId { get; set; }
		public virtual CategoriaTributaria? CategoriaTributaria { get; set; }
		[ForeignKey(nameof(Ciudad))]
		public int CiudadId { get; set; }
		public virtual Ciudad? Ciudad { get; set; }
		

	}
}
