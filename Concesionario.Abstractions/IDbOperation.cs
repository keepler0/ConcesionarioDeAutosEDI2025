namespace Concesionario.Abstractions
{
	public interface IDbOperation<T> where T : class
	{
		T Save(T Entity);
		IList<T> GetAll();
		T GetById(int Id);
		void Delete(int Id);
	}
}
