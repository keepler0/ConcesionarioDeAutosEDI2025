using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Concesionario.Entities
{
	public class Empleado:IEntidad
	{
		public int Id { get; set; }
		[StringLength(20)]
		public string PrimerNombre { get; set; } = string.Empty;
		[StringLength(20)]
		public string SegundoNombre { get; set; } = string.Empty;
		[StringLength(20)]
		public string Apellido { get; set; } = string.Empty;
		[StringLength(8)]
		public string Dni { get; set; } = string.Empty;
		[StringLength(13)]
		public string CuitCuil { get; set; } = string.Empty;
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = string.Empty;
		[StringLength(100)]
		public string Direccion { get; set; } = string.Empty;
		public int NumeroCasa { get; set; }
		[ForeignKey(nameof(Sector))]
		public int SectorId { get; set; }
		public virtual Sector? Sector { get; set; }
		[ForeignKey(nameof(Rol))]
		public int RolId { get; set; }
		public virtual TipoRol? Rol { get; set; }
		[ForeignKey(nameof(Ciudad))]
		public int CiudadId { get; set; }
		public virtual Ciudad? Ciudad { get; set; }
		

	}
}
