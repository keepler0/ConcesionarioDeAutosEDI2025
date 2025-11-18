using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Carroceria;
using Concesionario.Application.Dtos.Estado;
using Concesionario.Entities;
using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class EstadosController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IApplication<Estado> _estado;
		private readonly ILogger<EstadosController> _logger;
		private readonly UserManager<User> _userManager;

		public EstadosController(IMapper mapper,
								 IApplication<Estado> estado,
								 ILogger<EstadosController> logger,
								 UserManager<User> userManager)
		{
			_mapper = mapper;
			_estado = estado;
			_logger = logger;
			_userManager = userManager;
		}
		[HttpGet]
		[Route("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<EstadoResponseDto>>(_estado.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Estado estado = _estado.GetById(id.Value);
			if (estado is null) return NotFound();
			return Ok(_mapper.Map<EstadoResponseDto>(estado));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(EstadoRequestDto estadoRequestDto)
		{
			var id = GetUserId();
			var user = GetUser(id);
			if (await _userManager.IsInRoleAsync(user, "Administrador"))
			{
				GetUserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var estado = _mapper.Map<Estado>(estadoRequestDto);
				_estado.Save(estado);
				return Ok(estado.Id);
			}
			return Unauthorized();
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, EstadoRequestDto estadoRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (await _userManager.IsInRoleAsync(user,"Administrador"))
			{
				GetUserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Estado estadoBack = _estado.GetById(id.Value);
				if (estadoBack is null) return NotFound();
				estadoBack = _mapper.Map<Estado>(estadoRequestDto);
				_estado.Save(estadoBack);
				return Ok(_mapper.Map<EstadoResponseDto>(estadoBack));
			}
			return Unauthorized();
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (await _userManager.IsInRoleAsync(user,"Administrador"))
			{
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Estado estadoBack = _estado.GetById(id.Value);
				if (estadoBack is null) return NotFound();
				_estado.Delete(estadoBack.Id);
				return Ok();
			}
			return Unauthorized();
		}
		private string GetUserId() => User.FindFirst("id")!.Value;
		private User GetUser(string id) => _userManager.FindByIdAsync(id).Result!;
		private void GetUserClaims()
		{
			var name = User.FindFirst("name");
			var claims = User.Claims;
		}
	}
}
