using Concesionario.Abstractions;

namespace Concesionario.Repository
{
	public interface IRepository<T> : IDbOperation<T> where T : class
    {
    }
	public class Repository<T>:IRepository<T> where T : class
	{
		IDbContext<T> _db;
        public Repository(IDbContext<T> db)
        {
            _db = db;
        }

		public void Delete(int Id)
		{
			_db.Delete(Id);
		}

		public IList<T> GetAll()
		{
			return _db.GetAll();
		}

		public T GetById(int Id)
		{
			return _db.GetById(Id);
		}

		public T Save(T Entity)
		{
			return _db.Save(Entity);
		}
	}
}
