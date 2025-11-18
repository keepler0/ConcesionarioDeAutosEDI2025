using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Auto;
using Concesionario.Entities;
using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Color = Concesionario.Entities.Color;

namespace Concesionario.WebApi.Controllers
{
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

		[HttpGet]
		[Route("Autos disponibles")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IList<AutoResponseDto>>(_auto.GetAll()));
		}

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
		[Authorize("Administrador,Usuario,Vendedor,Cliente")]
		public async Task<IActionResult> Crear(AutoRequestDto autoRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var auto = _mapper.Map<Auto>(autoRequestDto);
			if (auto is null) return BadRequest("oops! Ah ocurrido un error al crear el auto");
			_auto.Save(auto);
			return Ok(auto.Id);
		}

		[HttpPut]
		[Route("Editar")]
		[Authorize("Administrador,Usuario,Vendedor,Cliente")]
		public async Task<IActionResult> Editar(int? id, AutoRequestDto autoRequestDto)
		{
			if (!id.HasValue || !ModelState.IsValid) return BadRequest();
			var autoBack = _auto.GetById(id.Value);
			if (autoBack is null) return NotFound();

			autoBack = _mapper.Map<Auto>(autoRequestDto);
			_auto.Save(autoBack);
			return Ok(autoBack.Id);
		}
		[HttpDelete]
		[Route("Borrar")]
		[Authorize("Administrador,Usuario,Vendedor,Cliente")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var autoBack = _auto.GetById(id.Value);
			if (autoBack is null) return NotFound();
			_auto.Delete(autoBack.Id);
			return Ok();
		}
	}
}
