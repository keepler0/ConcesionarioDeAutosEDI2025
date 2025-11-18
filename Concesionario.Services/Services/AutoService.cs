using Concesionario.Application;
using Concesionario.Entities;
using Concesionario.Entities.MicrosoftIdentity;
using Concesionario.Exceptions;
using Concesionario.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Concesionario.Services.Services
{
	public class AutoService : IAutoService
	{
		private readonly IApplication<Auto> _autoRepo;
		private readonly UserManager<User> _userManager;

		public AutoService(IApplication<Auto> autoRepo, UserManager<User> userManager)
		{
			_autoRepo = autoRepo;
			_userManager = userManager;
		}

		public async Task Crear(Auto auto, User user)
		{
			if (await _userManager.IsInRoleAsync(user,"Administrador") || 
				await _userManager.IsInRoleAsync(user, "Usuario")||
				await _userManager.IsInRoleAsync(user, "Cliente")||
				await _userManager.IsInRoleAsync(user, "Vendedor"))
			{
				throw new Exceptions.ValidationException("Acceso denegado: permisos insuficientes");
			}
			if (auto.Anio<2005 || auto.Anio>Convert.ToInt32(DateTime.UtcNow.Year))
			{
				throw new FueraDeRangoException("Anio del vehiculo no comprendido por la empresa, validos desde 2000 hasta el anio actual");
			}
			if (auto.Kilometros<0 || auto.Kilometros>500000 )
			{
				throw new FueraDeRangoException("El valor de los kilometros no considerados por la empresa");
			}
			if (auto.Anio>=2005 && auto.Anio<2016)
			{
				if (!Regex.IsMatch(auto.Matricula, @"^([A-Z]{3}\s?[0-9]{3})$"))
				{
					throw new InvalidFormatException("El formato de la matricula es incorrecta");
				}
			}
			else
			{
				if (!Regex.IsMatch(auto.Matricula, @"^([A-Z]{2}\s?[0-9]{3}\s?[A-Z]{2})$"))
				{
					throw new InvalidFormatException("El formato de la matricula es incorrecta");
				}
			}
			if (auto.Precio<1000 || auto.Precio>1000000)
			{
				throw new FueraDeRangoException("El valor del vehiculo fuera de rango, comprendido entre mil a un millon");
			}
			//esta condicion es solo para uso en USD deberia hacer una tabla divisas para poder elejir otras divisas a operar
		}

		public Task Delete(int? id)
		{
			if (!id.HasValue) throw new ArgumentNullException("Id citado null");
			var auto = _autoRepo.GetById(id.Value);
			if (auto is null) throw new NotFoundException("Objeto no encontrado en la base de datos");
			_autoRepo.Delete(auto.Id);
			return Task.CompletedTask;
		}

		public Task Editar(int? id, Auto auto)
		{
			var autoBack = _autoRepo.GetById(id!.Value);
			if (autoBack is null) throw new NotFoundException("No se encontro el objeto");
			autoBack.SetModelo(auto.Modelo);
			autoBack.SetVersion(auto.Version);
			autoBack.SetAnio(auto.Anio);
			autoBack.SetKilometros(auto.Kilometros);
			autoBack.SetMatricula(auto.Matricula);
			autoBack.SetMotor(auto.Motor);
			autoBack.SetNumeroMotor(auto.NumeroMotor);
			autoBack.SetNumeroChasis(auto.NumeroChasis);
			autoBack.SetCantAsientos(auto.CantAsientos);
			autoBack.SetPrecio(auto.Precio);

			return Task.FromResult(_autoRepo.Save(autoBack));
		}

		public Task<IList<Auto>> GetAll(User user)
		{
			return (Task<IList<Auto>>)_autoRepo.GetAll();
		}

		public Task<Auto> GetById(int? id)
		{
			if (!id.HasValue) throw new ArgumentException("Error: Id vacio");
			var auto = _autoRepo.GetById(id.Value);
			if (auto is null) throw new NotFoundException("No se pudo encontrar el auto con el id citado");
			return Task.FromResult(auto);
		}
	}
}
