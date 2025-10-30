using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Color;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ColoresController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<ColoresController> _logger;
		private readonly IApplication<Color> _color;

		public ColoresController(IMapper mapper, ILogger<ColoresController> logger, IApplication<Color> color)
		{
			_mapper = mapper;
			_logger = logger;
			_color = color;
		}
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<ColorResponseDto>>(_color.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
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
			if (!ModelState.IsValid) return BadRequest();
			var color = _mapper.Map<Color>(colorRequestDto);
			_color.Save(color);
			return Ok(color.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, ColorResquestDto colorRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Color colorBack = _color.GetById(id.Value);
			if (colorBack is null) return NotFound();
			colorBack = _mapper.Map<Color>(colorRequestDto);
			_color.Save(colorBack);
			return Ok(_mapper.Map<ColorResponseDto>(colorBack));
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			Color colorBack = _color.GetById(id.Value);
			if (colorBack is null) return NotFound();
			_color.Delete(colorBack.Id);
			return Ok();
		}
	}
}
