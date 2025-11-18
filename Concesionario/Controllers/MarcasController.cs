using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Combustible;
using Concesionario.Application.Dtos.Marca;
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
	public class MarcasController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<MarcasController> _logger;
		private readonly IApplication<Marca> _marca;
		private readonly UserManager<User> _userManager;
		public MarcasController(IMapper maper,
								ILogger<MarcasController> logger,
								IApplication<Marca> marca,
								UserManager<User> userManager)
		{
			_mapper = maper;
			_logger = logger;
			_marca = marca;
			_userManager = userManager;
		}
		[HttpGet]
		[Route("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<MarcaResponseDto>>(_marca.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Marca marca = _marca.GetById(id.Value);
			if (marca is null) return NotFound();
			return Ok(_mapper.Map<MarcaResponseDto>(marca));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(MarcaRequestDto marcaRequestDto)
		{
			var id = GetUserId();
			var user = GetUser(id);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				UserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var marca = _mapper.Map<Marca>(marcaRequestDto);
				_marca.Save(marca);
				return Ok(marca.Id);
			}
			return Unauthorized();
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id,MarcaRequestDto marcaRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Marca marcaBack = _marca.GetById(id.Value);
				if (marcaBack is null) return NotFound();
				marcaBack = _mapper.Map<Marca>(marcaRequestDto);
				_marca.Save(marcaBack);
				return Ok(_mapper.Map<MarcaResponseDto>(marcaBack));
			}
			return Unauthorized();
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult>Borrar(int? id)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Marca marcaBack = _marca.GetById(id.Value);
				if (marcaBack is null) return NotFound();
				_marca.Delete(marcaBack.Id);
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
