using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Combustible;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CombustiblesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CombustiblesController> _logger;
		private readonly IApplication<Combustible> _combustible;

		public CombustiblesController(IMapper mapper,
									  ILogger<CombustiblesController> logger,
									  IApplication<Combustible> combustible)
		{
			_mapper = mapper;
			_logger = logger;
			_combustible = combustible;
		}

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<CombustibleResponseDto>>(_combustible.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
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
			if (!ModelState.IsValid) return BadRequest();
			var combustible = _mapper.Map<Combustible>(combustibleRequestDto);
			_combustible.Save(combustible);
			return Ok(combustible.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, CombustibleRequestDto combustibleRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Combustible combustibleBack = _combustible.GetById(id.Value);
			if (combustibleBack is null) return NotFound();
			combustibleBack = _mapper.Map<Combustible>(combustibleRequestDto);
			_combustible.Save(combustibleBack);
			return Ok();
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			Combustible combustibleBack = _combustible.GetById(id.Value);
			if (combustibleBack is null) return NotFound();
			_combustible.Delete(combustibleBack.Id);
			return Ok();
		}
	}
}
