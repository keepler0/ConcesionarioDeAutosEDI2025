using Concesionario.Entities;
using Concesionario.Entities.MicrosoftIdentity;

namespace Concesionario.Services.IServices
{
	public interface IAutoService
	{
		Task<IList<Auto>> GetAll(User user);
		Task<Auto> GetById(int? id);
		Task Crear(Auto auto, User user);
		Task Editar(int? id, Auto auto);
		Task Delete(int? id);
	}
}
