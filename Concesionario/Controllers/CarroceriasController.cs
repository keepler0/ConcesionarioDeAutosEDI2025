using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Carroceria;
using Concesionario.Entities;
using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class CarroceriasController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly ILogger<CarroceriasController> _logger;
		private readonly IApplication<Carroceria> _carroceria;
		private readonly IMapper _mapper;
		public CarroceriasController(ILogger<CarroceriasController> logger,
									 IApplication<Carroceria> carroceria,
									 IMapper mapper,
									 UserManager<User> userManager)
		{
			_logger = logger;
			_mapper = mapper;
			_carroceria = carroceria;
			_userManager = userManager;
		}
		[AllowAnonymous]
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<CarroceriaResponseDto>>(_carroceria.GetAll()));
		}
		[AllowAnonymous]
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Carroceria carroceria = _carroceria.GetById(id.Value);
			if (carroceria is null) return NotFound();
			return Ok(_mapper.Map<CarroceriaResponseDto>(carroceria));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(CarroceriaRequestDto carroceriaRequestDto)
		{
			var id = GetUserId();
			var user = GetUser(id);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				GetUserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var carroceria = _mapper.Map<Carroceria>(carroceriaRequestDto);
				_carroceria.Save(carroceria);
				return Ok(carroceria.Id);
			}
			return Unauthorized();

		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, CarroceriaRequestDto carroceriaRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				GetUserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				var carroceriaBack = _carroceria.GetById(id.Value);
				if (carroceriaBack is null) return NotFound();
				carroceriaBack = _mapper.Map<Carroceria>(carroceriaRequestDto);
				_carroceria.Save(carroceriaBack);
				return Ok(_mapper.Map<CarroceriaResponseDto>(carroceriaBack));
			}
			return Unauthorized();
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				GetUserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();

				var carroceriaBack = _carroceria.GetById(id.Value);
				if (carroceriaBack is null) return NotFound();
				_carroceria.Delete(carroceriaBack.Id);
				return Ok();
			}
			return Unauthorized();
		}
		//TODO: Preguntar si esto es una buena practica o no
		//TODO: PReguntar si es recomendable realizar una clase statica en Service para reutilizar codigo
		private string GetUserId() => User.FindFirst("id")!.Value;
		private User GetUser(string id) => _userManager.FindByIdAsync(id).Result!;
		private void GetUserClaims()
		{
			var name = User.FindFirst("name");
			var claims = User.Claims;
		}
	}
}
