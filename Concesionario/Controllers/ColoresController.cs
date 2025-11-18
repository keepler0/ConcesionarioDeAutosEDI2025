using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Color;
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
	public class ColoresController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<ColoresController> _logger;
		private readonly IApplication<Color> _color;
		private readonly UserManager<User> _userManager;

		public ColoresController(IMapper mapper, 
								 ILogger<ColoresController> logger, 
								 IApplication<Color> color,
								 UserManager<User> userManager)
		{
			_mapper = mapper;
			_logger = logger;
			_color = color;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("GetAll")]
		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<ColorResponseDto>>(_color.GetAll()));
		}

		[HttpGet]
		[Route("ByID")]
		[AllowAnonymous]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Color color = _color.GetById(id.Value);
			if (color is null) return NotFound();
			return Ok(_mapper.Map<ColorResponseDto>(color));
		}

		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(ColorResquestDto colorRequestDto)
		{
			var id = GetUserId();
			var user = GetUser(id);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				GetUserClaims();
				if (!ModelState.IsValid) return BadRequest();
				var color = _mapper.Map<Color>(colorRequestDto);
				_color.Save(color);
				return Ok(color.Id);
			}
			return Unauthorized();
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, ColorResquestDto colorRequestDto)
		{
			var userId = GetUserId();
			var user = GetUser(userId);
			if (_userManager.IsInRoleAsync(user,"Administrador").Result)
			{
				GetUserClaims();
				if (!id.HasValue|| !ModelState.IsValid) return BadRequest();
				var colorBack = _color.GetById(id.Value);
				if (colorBack is null) return NotFound();
				colorBack = _mapper.Map<Color>(colorRequestDto);
				_color.Save(colorBack);
				return Ok(_mapper.Map<ColorResponseDto>(colorBack));
			}
			return Unauthorized();
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
				var colorBack = _color.GetById(id.Value);
				if (colorBack is null) return NotFound();
				_color.Delete(colorBack.Id);
				return Ok();
			}
			return Unauthorized();
		}
		private string GetUserId() => User.FindFirst("id")!.Value.ToString();
		private User GetUser(string id) => _userManager.FindByIdAsync(id).Result!;
		private void GetUserClaims()
		{
			var name = User.FindFirst("name");
			var claims = User.Claims;
		}
	}
}
