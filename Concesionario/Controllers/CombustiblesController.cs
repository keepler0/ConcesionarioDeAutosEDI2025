using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Combustible;
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
	public class CombustiblesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CombustiblesController> _logger;
		private readonly IApplication<Combustible> _combustible;
		private readonly UserManager<User> _userManager;

		public CombustiblesController(IMapper mapper,
									  ILogger<CombustiblesController> logger,
									  IApplication<Combustible> combustible,
									  UserManager<User> userManager)
		{
			_mapper = mapper;
			_logger = logger;
			_combustible = combustible;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<CombustibleResponseDto>>(_combustible.GetAll()));
		}
		
		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Combustible combustible = _combustible.GetById(id.Value);
			if (combustible is null) return NotFound();
			return Ok(_mapper.Map<CombustibleResponseDto>(combustible));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(CombustibleRequestDto combustibleRequestDto)
		{
			var id = GetUserId();
			var user = GetUser(id);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				GetUserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var combustible = _mapper.Map<Combustible>(combustibleRequestDto);
				_combustible.Save(combustible);
				return Ok(combustible.Id);
			}
			return Unauthorized();
		}

		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, CombustibleRequestDto combustibleRequestDto)
		{
<<<<<<< Updated upstream
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Combustible combustibleBack = _combustible.GetById(id.Value);
			if (combustibleBack is null) return NotFound();
			combustibleBack = _mapper.Map<Combustible>(combustibleRequestDto);
			_combustible.Save(combustibleBack);
			return Ok();
=======
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				GetUserClaims();
				if (!id.HasValue|| !ModelState.IsValid) return BadRequest();
				if (!ModelState.IsValid) return BadRequest();
				var combustibleBack = _combustible.GetById(id.Value);
				if (combustibleBack is null) return NotFound();
				combustibleBack = _mapper.Map<Combustible>(combustibleRequestDto);
				_combustible.Save(combustibleBack);
				return Ok(_mapper.Map<CombustibleResponseDto>(combustibleBack));
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
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				GetUserClaims();
				if (!id.HasValue || !ModelState.IsValid) return BadRequest();
				var combustibleBack = _combustible.GetById(id.Value);
				if (combustibleBack is null) return NotFound();
				_combustible.Delete(combustibleBack.Id);
				return Ok();
			}
			return Unauthorized();
		}
		[NonAction]
		public string GetUserId() => User.FindFirst("id")!.Value.ToString();
		[NonAction]
		public User GetUser(string id) => _userManager.FindByIdAsync(id).Result!;
		[NonAction]
		public void GetUserClaims()
		{
			var name = User.FindFirst("name");
			var claims = User.Claims;
		}
	}
}
