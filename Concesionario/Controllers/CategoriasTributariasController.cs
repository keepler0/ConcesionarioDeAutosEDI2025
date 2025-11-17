using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.CategoriaTributaria;
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
	public class CategoriasTributariasController : ControllerBase
	{
		private readonly ILogger<CategoriasTributariasController> _logger;
		private readonly IApplication<CategoriaTributaria> _ct;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		public CategoriasTributariasController(ILogger<CategoriasTributariasController> logger,
											   IApplication<CategoriaTributaria> ct,
											   IMapper mapper,
											   UserManager<User> userManager)
		{
			_ct = ct;
			_logger = logger;
			_mapper = mapper;
			_userManager = userManager;
		}
		[HttpGet]
		[Route("All")]
		[AllowAnonymous]
		public async Task<IActionResult> All()=>Ok(_mapper.Map<IList<CategoriaTributariaResponseDto>>(_ct.GetAll()));

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var ct = _ct.GetById(id.Value);
			if (ct is null) return NotFound();
			return Ok(_mapper.Map<CategoriaTributariaResponseDto>(ct));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(CategoriaTributariaRequestDto ctRequestDto)
		{
			var id = GetId();
			var user = GetUser(id);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				GetUserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var CT = _mapper.Map<CategoriaTributaria>(ctRequestDto);
				_ct.Save(CT);
				return Ok(CT.Id);
			}
			return Unauthorized();

		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, CategoriaTributariaRequestDto ctRequestDto)
		{
<<<<<<< Updated upstream
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			CategoriaTributaria ctBack = _ct.GetById(id.Value);
			if (ctBack is null) return NotFound();
			ctBack = _mapper.Map<CategoriaTributaria>(ctRequestDto);
			_ct.Save(ctBack);
			return Ok();
=======
			var userId = GetId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				GetUserClaims();
				if (!id.HasValue) return BadRequest();
				if (!ModelState.IsValid) return BadRequest();
				CategoriaTributaria ctBack = _ct.GetById(id.Value);
				if (ctBack is null) return NotFound();
				ctBack = _mapper.Map<CategoriaTributaria>(ctRequestDto);
				_ct.Save(ctBack);
				return Ok(_mapper.Map<CategoriaTributariaResponseDto>(ctBack));
			}
			return Unauthorized();
>>>>>>> Stashed changes
		}

		[HttpDelete]
		[Route("Borrar")]
		public async Task<IActionResult> Borrar(int? id)
		{
			var userId = GetId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user, "Administrador").Result)
			{
				GetUserClaims();
				if (!id.HasValue) return BadRequest();
				if (!ModelState.IsValid) return BadRequest();

				CategoriaTributaria ctBack = _ct.GetById(id.Value);
				if (ctBack is null) return NotFound();
				_ct.Delete(ctBack.Id);
				return Ok();
			}
			return Unauthorized();
		}
		
		private User GetUser(string id) => _userManager.FindByIdAsync(id).Result!;
		private string GetId() => User.FindFirst("id")!.Value.ToString();
		private void GetUserClaims()
		{
			var name = User.FindFirst("name");
			var claims = User.Claims;
		}
	}
}
