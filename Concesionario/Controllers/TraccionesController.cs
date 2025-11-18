using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Traccion;
using Concesionario.Entities;
using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class TraccionesController : ControllerBase
	{
		private readonly ILogger<TraccionesController> _logger;
		private readonly IApplication<Traccion> _traccion;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public TraccionesController(ILogger<TraccionesController> logger,
									IApplication<Traccion> traccion,
									IMapper mapper,
									UserManager<User> userManager)
		{
			_logger = logger;
			_traccion = traccion;
			_mapper = mapper;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<TraccionResponseDto>>(_traccion.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Traccion traccion = _traccion.GetById(id.Value);
			if (traccion is null) return NotFound();
			return Ok(_mapper.Map<TraccionResponseDto>(traccion));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TraccionRequestDto traccionRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				UserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var traccion = _mapper.Map<Traccion>(traccionRequestDto);
				_traccion.Save(traccion);
				return Ok(traccion.Id);
			}
			return Unauthorized();
			
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TraccionRequestDto traccionRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Traccion traccionBack = _traccion.GetById(id.Value);
				if (traccionBack is null) return NotFound();
				traccionBack = _mapper.Map<Traccion>(traccionRequestDto);
				_traccion.Save(traccionBack);
				return Ok(_mapper.Map<TraccionResponseDto>(traccionBack));
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
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Traccion traccionBack = _traccion.GetById(id.Value);
				if (traccionBack is null) return NotFound();
				_traccion.Delete(traccionBack.Id);
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
