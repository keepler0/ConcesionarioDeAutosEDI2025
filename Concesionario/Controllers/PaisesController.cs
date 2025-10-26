using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Pais;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaisesController : ControllerBase
	{
		private readonly ILogger<PaisesController> _logger;
		private readonly IApplication<Pais> _pais;
		private readonly IMapper _mapper;
		public PaisesController(ILogger<PaisesController> logger,
								IApplication<Pais> pais,
								IMapper mapper)
		{
			_logger = logger;
			_pais = pais;
			_mapper = mapper;
		}
		[HttpGet]
		[Route("All")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<PaisResponseDto>>(_pais.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Pais pais = _pais.GetById(id.Value);
			if (pais is null) return NotFound();
			return Ok(_mapper.Map<PaisResponseDto>(pais));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(PaisRequestDto paisRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var pais = _mapper.Map<Pais>(paisRequestDto);
			_pais.Save(pais);
			return Ok(pais.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, PaisRequestDto paisRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Pais paisBack = _pais.GetById(id.Value);
			if (paisBack is null) return NotFound();
			paisBack = _mapper.Map<Pais>(paisRequestDto);
			_pais.Save(paisBack);
			return Ok();
		}
		[HttpDelete]
		[Route("Borrar")]

		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			Pais paisBack = _pais.GetById(id.Value);
			if (paisBack is null) return NotFound();
			_pais.Delete(paisBack.Id);
			return Ok();
		}
	}
}
