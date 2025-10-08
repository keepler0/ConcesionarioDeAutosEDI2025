using Concesionario.Abstractions;
using Concesionario.Repository;

namespace Concesionario.Application
{
	public interface IApplication<T> : IDbOperation<T> where T: class { }
	public class Application<T> : IApplication<T> where T: class
	{
		private IRepository<T> _repository;
		public Application(IRepository<T> repository)
		{
			_repository = repository;
		}

		public void Delete(int Id)
		{
			_repository.Delete(Id);
		}

		public IList<T> GetAll()
		{
			return _repository.GetAll();
		}

		public T GetById(int Id)
		{
			return _repository.GetById(Id);
		}

		public T Save(T Entity)
		{
			return _repository.Save(Entity);
		}
	}
}
