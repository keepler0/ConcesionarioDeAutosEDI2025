using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Carroceria;
using Concesionario.Application.Dtos.Estado;
using Concesionario.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EstadosController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IApplication<Estado> _estado;
		private readonly ILogger<EstadosController> _logger;

		public EstadosController(IMapper mapper, 
								 IApplication<Estado> estado, 
								 ILogger<EstadosController> logger)
		{
			_mapper = mapper;
			_estado = estado;
			_logger = logger;
		}
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<EstadoResponseDto>>(_estado.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Estado estado = _estado.GetById(id.Value);
			if (estado is null) return NotFound();
			return Ok(_mapper.Map<EstadoResponseDto>(estado));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(EstadoRequestDto estadoRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var estado = _mapper.Map<Estado>(estadoRequestDto);
			_estado.Save(estado);
			return Ok(estado.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, EstadoRequestDto estadoRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Estado estadoBack = _estado.GetById(id.Value);
			if (estadoBack is null) return NotFound();
			estadoBack = _mapper.Map<Estado>(estadoRequestDto);
			_estado.Save(estadoBack);
			return Ok(_mapper.Map<EstadoResponseDto>(estadoBack));
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			Estado estadoBack = _estado.GetById(id.Value);
			if (estadoBack is null) return NotFound();
			_estado.Delete(estadoBack.Id);
			return Ok();
		}
	}
}
