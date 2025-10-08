namespace Concesionario.Abstractions
{
	public interface ICaracteristicaAuto<T> where T : class
	{
        public string Descripcion { get; set; }
        public ICollection<T> Autos { get; set; }
    }
}
