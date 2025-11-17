using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Transmision;
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
	public class TransmisionesController : ControllerBase
	{
		private readonly IApplication<Transmision> _transmision;
		private readonly ILogger<TransmisionesController> _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public TransmisionesController(IApplication<Transmision> transmision,
									   ILogger<TransmisionesController> logger,
									   IMapper mapper,
									   UserManager<User> userManager)
		{
			_transmision = transmision;
			_logger = logger;
			_mapper = mapper;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("All")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IList<TransmisionResponseDto>>(_transmision.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> GetById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Transmision transmision = _transmision.GetById(id.Value);
			if (transmision is null) return NotFound();
			return Ok(_mapper.Map<TransmisionResponseDto>(transmision));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TransmisionRequestDto transmisionRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				UserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var transmision = _mapper.Map<Transmision>(transmisionRequestDto);
				_transmision.Save(transmision);
				return Ok(transmision.Id);
			}
			return Unauthorized();
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TransmisionRequestDto transmisionRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				Transmision transmisionBack = _transmision.GetById(id.Value);
				if (transmisionBack is null) return NotFound();
				transmisionBack = _mapper.Map<Transmision>(transmisionRequestDto);
				_transmision.Save(transmisionBack);
				return Ok(_mapper.Map<TransmisionResponseDto>(transmisionBack));
			}
			return Unauthorized();
		}

		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Delete(int? id)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				UserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				var transmisionBack = _transmision.GetById(id.Value);
				if (transmisionBack is null) return NotFound();
				_transmision.Delete(transmisionBack.Id);
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
