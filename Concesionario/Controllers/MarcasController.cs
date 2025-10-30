using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Combustible;
using Concesionario.Application.Dtos.Marca;
using Concesionario.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MarcasController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<MarcasController> _logger;
		private readonly IApplication<Marca> _marca;
		public MarcasController(IMapper maper,
								ILogger<MarcasController> logger,
								IApplication<Marca> marca)
		{
			_mapper = maper;
			_logger = logger;
			_marca = marca;
		}
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<MarcaResponseDto>>(_marca.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Marca marca = _marca.GetById(id.Value);
			if (marca is null) return NotFound();
			return Ok(_mapper.Map<MarcaResponseDto>(marca));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(MarcaRequestDto marcaRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var marca = _mapper.Map<Marca>(marcaRequestDto);
			_marca.Save(marca);
			return Ok(marca.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id,MarcaRequestDto marcaRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Marca marcaBack=_marca.GetById(id.Value);
			if (marcaBack is null) return NotFound();
			marcaBack = _mapper.Map<Marca>(marcaRequestDto);
			_marca.Save(marcaBack);
			return Ok(_mapper.Map<MarcaResponseDto>(marcaBack));
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult>Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			Marca marcaBack = _marca.GetById(id.Value);
			if (marcaBack is null) return NotFound();
			_marca.Delete(marcaBack.Id);
			return Ok();
		}
    }
}
