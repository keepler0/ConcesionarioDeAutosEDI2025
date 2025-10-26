using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Traccion;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TraccionesController : ControllerBase
	{
		private readonly ILogger<TraccionesController> _logger;
		private readonly IApplication<Traccion> _traccion;
		private readonly IMapper _mapper;

		public TraccionesController(ILogger<TraccionesController> logger,
									IApplication<Traccion> traccion,
									IMapper mapper)
		{
			_logger = logger;
			_traccion = traccion;
			_mapper = mapper;
		}
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<TraccionResponseDto>>(_traccion.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Traccion traccion = _traccion.GetById(id.Value);
			if (traccion is null) return NotFound();
			return Ok(_mapper.Map<TraccionResponseDto>(traccion));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TraccionRequestDto traccionRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var traccion = _mapper.Map<Traccion>(traccionRequestDto);
			_traccion.Save(traccion);
			return Ok(traccion.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TraccionRequestDto traccionRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Traccion traccionBack = _traccion.GetById(id.Value);
			if (traccionBack is null) return NotFound();
			traccionBack = _mapper.Map<Traccion>(traccionRequestDto);
			_traccion.Save(traccionBack);
			return Ok();
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			Traccion traccionBack = _traccion.GetById(id.Value);
			if (traccionBack is null) return NotFound();
			_traccion.Delete(traccionBack.Id);
			return Ok();
		}
	}
}
