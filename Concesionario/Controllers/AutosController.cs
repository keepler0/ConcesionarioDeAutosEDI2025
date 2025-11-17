using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Auto;
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
	public class AutosController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly ILogger<AutosController> _logger;
		private readonly IMapper _mapper;
		private readonly IApplication<Auto> _auto;

		public AutosController(UserManager<User> userManager,
							   ILogger<AutosController> logger,
							   IMapper mapper,
							   IApplication<Auto> auto)
		{
			_userManager = userManager;
			_logger = logger;
			_mapper = mapper;
			_auto = auto;
		}

		[AllowAnonymous]
		[HttpGet]
		[Route("Autos disponibles")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IList<AutoResponseDto>>(_auto.GetAll()));
		}
		[AllowAnonymous]
		[HttpGet]
		[Route("GetById")]
		public async Task<IActionResult> GetById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var auto = _auto.GetById(id.Value);
			if (auto is null) return NotFound();
			return Ok(_mapper.Map<AutoResponseDto>(auto));
		}
		[HttpPost]
		[Route("AgregarAuto")]
		public async Task<IActionResult> Crear(AutoRequestDto autoRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result ||
				_userManager.IsInRoleAsync(user, "Usuario").Result)
			{
				UserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var auto = _mapper.Map<Auto>(autoRequestDto);
				_auto.Save(auto);
				return Ok(auto.Id);
			}
			return Unauthorized();
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, AutoRequestDto autoRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result ||
				_userManager.IsInRoleAsync(user, "Usuario").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				var autoBack = _auto.GetById(id.Value);
				if (autoBack is null) return NotFound();

				autoBack = _mapper.Map<Auto>(autoRequestDto);
				_auto.Save(autoBack);
				return Ok(autoBack.Id);
			}
			return Unauthorized();
		}
		[HttpDelete]
		[Route("Borrar")]
		public async Task<IActionResult> Borrar(int? id)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result ||
				_userManager.IsInRoleAsync(user, "Usuario").Result)
			{
				UserClaims();
				if (!id.HasValue) return BadRequest();
				var autoBack = _auto.GetById(id.Value);
				if (autoBack is null) return NotFound();
				_auto.Delete(autoBack.Id);
				return Ok();
			}
			return Unauthorized();
		}
		private string GetUserId() => User.FindFirst("id")!.Value;
		private User GetUser(string id) => _userManager.FindByIdAsync(id).Result!;
		private void UserClaims()
		{
			var name = User.FindFirst("name");
			var claims = User.Claims;
		}
	}
}
