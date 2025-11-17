using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.TipoDePago;
using Concesionario.Application.Dtos.TipoDeVenta;
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
	public class TiposDeVentasController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<TiposDeVentasController> _logger;
		private readonly IApplication<TipoDeVenta> _tipoDeVenta;
		private readonly UserManager<User> _userManager;

		public TiposDeVentasController(IMapper mapper,
									   ILogger<TiposDeVentasController> logger,
									   IApplication<TipoDeVenta> tipoDeVenta,
									   UserManager<User> userManager)
		{
			_mapper = mapper;
			_logger = logger;
			_tipoDeVenta = tipoDeVenta;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IList<TipoDeVentaResponseDto>>(_tipoDeVenta.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult>GetById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			TipoDeVenta tipoDeVenta = _tipoDeVenta.GetById(id.Value);
			if (tipoDeVenta is null) return NotFound();
			return Ok(_mapper.Map<TipoDeVentaResponseDto>(tipoDeVenta));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TipoDeVentaRequestDto tipoDeVentaRequestDto)
		{
			var id = GetUserId();
			var user = GetUser(id);
			if (_userManager.IsInRoleAsync(user,"Adiministrador").Result)
			{
				UserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var tipoDeVenta = _mapper.Map<TipoDeVenta>(tipoDeVentaRequestDto);
				_tipoDeVenta.Save(tipoDeVenta);
				return Ok(tipoDeVenta.Id);
			}
			return Unauthorized();
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TipoDeVentaRequestDto tipoDeVentaRequestDto)
		{
<<<<<<< Updated upstream
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			TipoDeVenta tipoDeVentaBack = _tipoDeVenta.GetById(id.Value);
			if (tipoDeVentaBack is null) return NotFound();
			tipoDeVentaBack = _mapper.Map<TipoDeVenta>(tipoDeVentaRequestDto);
			_tipoDeVenta.Save(tipoDeVentaBack);
			return Ok();
=======
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Adiministrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				TipoDeVenta tipoDeVentaBack = _tipoDeVenta.GetById(id.Value);
				if (tipoDeVentaBack is null) return NotFound();
				tipoDeVentaBack = _mapper.Map<TipoDeVenta>(tipoDeVentaRequestDto);
				_tipoDeVenta.Save(tipoDeVentaBack);
				return Ok(_mapper.Map<TipoDePagoResponseDto>(tipoDeVentaBack));
			}
			return Unauthorized();
>>>>>>> Stashed changes
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Adiministrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				TipoDeVenta tipoDeVentaBack = _tipoDeVenta.GetById(id.Value);
				if (tipoDeVentaBack is null) return NotFound();
				_tipoDeVenta.Delete(tipoDeVentaBack.Id);
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
