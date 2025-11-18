using Concesionario.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Concesionario.Entities
{
	public class Auto : IEntidad
	{
		public Auto()
		{
			DetallesVentas = new HashSet<DetalleVenta>();
		}
		#region Getters
		public int Id { get; set; }
		[StringLength(50)]
		public string Modelo { get; private set; }
		[StringLength(20)]
		public string Version { get; private set; }
		public int Anio { get; private set; }
		public int Kilometros { get; private set; }
		[StringLength(7)]
		public string Matricula { get; private set; }
		[StringLength(50)]
		public string Motor { get; private set; }
		[StringLength(30)]
		//[Index(IsUnique=true)]
		public string NumeroMotor { get; private set; }
		[StringLength(17)]
		public string NumeroChasis { get; private set; }
		public int CantAsientos { get; private set; }
		[DataType(DataType.Currency)]
		public decimal Precio { get; private set; }
		#endregion

		#region Setters
		public void SetModelo(string modelo)
		{
			if (string.IsNullOrWhiteSpace(modelo)) 
				throw new ArgumentException("Campo vacio MODELO");
			Modelo = modelo;
		}
		public void SetVersion(string version)
		{
			if (string.IsNullOrWhiteSpace(version)) 
				throw new ArgumentException("Campo vacio VERSION");
			Version = version;
		}
		public void SetAnio(int anio)
		{
			Anio = anio;
		}
		public void SetKilometros(int kilometros)
		{
			Kilometros = kilometros;
		}
		public void SetMatricula(string matricula)
		{
			if (string.IsNullOrWhiteSpace(matricula)) 
				throw new ArgumentException("Campo vacio MATRICULA");
			Matricula = matricula;
		}
		public void SetMotor(string motor)
		{
			if (string.IsNullOrWhiteSpace(motor)) 
				throw new ArgumentException("Campo vacio MOTOR");
			Motor=motor;
		}
		public void SetNumeroMotor(string numeroMotor)
		{
			if (string.IsNullOrWhiteSpace(numeroMotor)) 
				throw new ArgumentException("Campo vacio NUMERO De MOTOR");
			NumeroMotor = numeroMotor;
		}
		public void SetNumeroChasis(string numeroChasis)
		{
			if (string.IsNullOrWhiteSpace(numeroChasis)) 
				throw new ArgumentException("Campo vacio NUMERO De CHASIS");
			NumeroChasis = numeroChasis;
		}
		public void SetCantAsientos(int cantAsientos)
		{
			if (cantAsientos<0) throw new ArgumentException("Cantidad de asientos irrisorio");
			CantAsientos = cantAsientos;
		}
		public void SetPrecio(decimal precio)
		{
			Precio= precio;
		}
		#endregion

		#region ForeignKey
		[ForeignKey(nameof(Marca))]
		public int MarcaId { get; set; }
		[ForeignKey(nameof(Carroceria))]
		public int CarroceriaId { get; set; }
		[ForeignKey(nameof(Color))]
		public int ColorId { get; set; }
		[ForeignKey(nameof(Combustible))]
		public int CombustibleId { get; set; }
		[ForeignKey(nameof(Estado))]
		public int EstadoId { get; set; }
		[ForeignKey(nameof(Pais))]
		public int PaisId { get; set; }
		[ForeignKey(nameof(Traccion))]
		public int TraccionId { get; set; }
		[ForeignKey(nameof(Transmision))]
		public int TransmisionId { get; set; }
		#endregion

		#region Virtual
		public virtual Marca? Marca { get; set; }
		public virtual Carroceria? Carroceria { get; set; }
		public virtual Color? Color { get; set; }
		public virtual Combustible? Combustible { get; set; }
		public virtual Estado? Estado { get; set; }
		public virtual Pais? Pais { get; set; }
		public virtual Traccion? Traccion { get; set; }
		public virtual Transmision? Transmision { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVentas { get; set; }
		#endregion
	}
}
