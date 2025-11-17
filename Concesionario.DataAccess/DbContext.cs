using Concesionario.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Concesionario.DataAccess
{
	//TODO: Agregar Try/Catch
	public class DbContext<T>:IDbContext<T> where T : class, IEntidad
	{
		DbSet<T> _Items;
		DbDataAccess _ctx;
        public DbContext(DbDataAccess ctx)
        {
            _ctx = ctx;
            _Items = _ctx.Set<T>();
        }

		public void Delete(int Id)
		{
			var entity = _Items.FirstOrDefault(i => i.Id == Id);
            if (entity is not null)
            {
               _Items.Remove(entity);
            }
			_ctx.SaveChanges();
        }

		public IList<T> GetAll()
		{
			return _Items.ToList();
		}

		public T GetById(int Id)
		{
			return _Items.FirstOrDefault(i => i.Id == Id)!;
		}

		public T Save(T Entity)
		{
			if (Entity.Id.Equals(0))
			{
				_Items.Add(Entity);
			}
			else
			{
				var entityInDb = GetById(Entity.Id);
				_ctx.Entry(entityInDb).State= EntityState.Detached;
				_Items.Update(Entity);
			}
			_ctx.SaveChanges();
			return Entity;
		}
	}
}
