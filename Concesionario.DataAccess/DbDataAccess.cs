using Concesionario.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Concesionario.DataAccess
{
	public class DbDataAccess : IdentityDbContext
	{
		public virtual DbSet<Auto> Autos { get; set; }
		public virtual DbSet<Pais> Paises { get; set; }
		public virtual DbSet<Provincia> Provincias { get; set; }
		public virtual DbSet<Ciudad> Ciudades { get; set; }
		public virtual DbSet<Combustible> Combustibles { get; set; }
		public virtual DbSet<Color> Colores { get; set; }
		public virtual DbSet<Cliente> Clientes { get; set; }
		public virtual DbSet<Empleado> Empleados { get; set; }
		public virtual DbSet<Estado> Estados { get; set; }
		public virtual DbSet<TipoRol> TipoRol { get; set; }
		public virtual DbSet<Sector> Sectores { get; set; }
		public virtual DbSet<Venta> Ventas { get; set; }
		public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }
		public virtual DbSet<Transmision> Transmisiones { get; set; }
		public virtual DbSet<Traccion> Tracciones { get; set; }
		public virtual DbSet<TipoDePago> TiposDePagos { get; set; }
		public virtual DbSet<TipoDeVenta> TiposDeVentas { get; set; }
		public virtual DbSet<Carroceria> Carrocerias { get; set; }
		public virtual DbSet<CategoriaTributaria> CategoriasTibutarias { get; set; }
		public virtual DbSet<Marca> Marcas { get; set; }
		public DbDataAccess(DbContextOptions<DbDataAccess> options) : base(options)
		{
		}
<<<<<<< Updated upstream
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine).EnableDetailedErrors();

=======
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
																	  optionsBuilder.LogTo(Console.WriteLine)
																					.EnableDetailedErrors();
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
		}
>>>>>>> Stashed changes
	}
}
