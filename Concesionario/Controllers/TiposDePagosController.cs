using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.TipoDePago;
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
	public class TiposDePagosController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IApplication<TipoDePago> _tipoDePago;
		private readonly ILogger<TiposDePagosController> _logger;
		private readonly UserManager<User> _userManager;

		public TiposDePagosController(IMapper mapper,
									  IApplication<TipoDePago> tipoDePago,
									  ILogger<TiposDePagosController> logger,
									  UserManager<User> userManager)
		{
			_mapper = mapper;
			_tipoDePago = tipoDePago;
			_logger = logger;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<TipoDePagoResponseDto>>(_tipoDePago.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			TipoDePago tipoDePago = _tipoDePago.GetById(id.Value);
			if (tipoDePago is null) return NotFound();
			return Ok(_mapper.Map<TipoDePagoResponseDto>(tipoDePago));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TipoDePagoRequestDto tipoDePagoRequestDto)
		{
			var id = GetUserId();
			var user = GetUser(id);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				UserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var tipoDePago = _mapper.Map<TipoDePago>(tipoDePagoRequestDto);
				_tipoDePago.Save(tipoDePago);
				return Ok(tipoDePago.Id);
			}
			return Unauthorized();
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TipoDePagoRequestDto tipoDePagoRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				TipoDePago tipoDePagoBack = _tipoDePago.GetById(id.Value);
				if (tipoDePagoBack is null) return NotFound();
				tipoDePagoBack = _mapper.Map<TipoDePago>(tipoDePagoRequestDto);
				_tipoDePago.Save(tipoDePagoBack);
				return Ok(_mapper.Map<TipoDePagoResponseDto>(tipoDePagoBack));
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
				if (!id.HasValue) return BadRequest();
				if (!ModelState.IsValid) return BadRequest();

				TipoDePago tipoDePagoBack = _tipoDePago.GetById(id.Value);
				if (tipoDePagoBack is null) return NotFound();
				_tipoDePago.Delete(tipoDePagoBack.Id);
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
