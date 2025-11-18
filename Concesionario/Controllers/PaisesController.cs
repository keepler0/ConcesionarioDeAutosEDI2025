using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Pais;
using Concesionario.Entities;
using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Concesionario.WebApi.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class PaisesController : ControllerBase
	{
		private readonly ILogger<PaisesController> _logger;
		private readonly IApplication<Pais> _pais;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		public PaisesController(ILogger<PaisesController> logger,
								IApplication<Pais> pais,
								IMapper mapper,
								UserManager<User> userManager)
		{
			_logger = logger;
			_pais = pais;
			_mapper = mapper;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("All")]
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<PaisResponseDto>>(_pais.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Pais pais = _pais.GetById(id.Value);
			if (pais is null) return NotFound();
			return Ok(_mapper.Map<PaisResponseDto>(pais));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(PaisRequestDto paisRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				UserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var pais = _mapper.Map<Pais>(paisRequestDto);
				_pais.Save(pais);
				return Ok(pais.Id);
			}
			return Unauthorized();
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, PaisRequestDto paisRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Pais paisBack = _pais.GetById(id.Value);
				if (paisBack is null) return NotFound();
				paisBack = _mapper.Map<Pais>(paisRequestDto);
				_pais.Save(paisBack);
				return Ok(_mapper.Map<PaisResponseDto>(paisBack)); 
			}
			return Unauthorized();
		}

		[HttpDelete]
		[Route("Borrar")]
		public async Task<IActionResult> Borrar(int? id)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Pais paisBack = _pais.GetById(id.Value);
				if (paisBack is null) return NotFound();
				_pais.Delete(paisBack.Id);
				return Ok();
			}
			return Unauthorized();
		}
		private string GetUserId() => User.FindFirst("id")!.Value.ToString();
		private User GetUser(string id) => _userManager.FindByIdAsync(id).Result!;
		private void UserClaims()
		{
			var name = User.FindFirst("name");
			var claims = User.Claims;
		}
		
	}
}
