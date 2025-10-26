using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Carroceria;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarroceriasController : ControllerBase
	{
		private readonly ILogger<CarroceriasController> _logger;
		private readonly IApplication<Carroceria> _carroceria;
		private readonly IMapper _mapper;
		public CarroceriasController(ILogger<CarroceriasController> logger,
									 IApplication<Carroceria> carroceria,
									 IMapper mapper)
		{
			_logger = logger;
			_mapper = mapper;
			_carroceria = carroceria;
		}
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<CarroceriaResponseDto>>(_carroceria.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Carroceria carroceria = _carroceria.GetById(id.Value);
			if (carroceria is null) return NotFound();
			return Ok(_mapper.Map<CarroceriaResponseDto>(carroceria));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(CarroceriaRequestDto carroceriaRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var carroceria = _mapper.Map<Carroceria>(carroceriaRequestDto);
			_carroceria.Save(carroceria);
			return Ok(carroceria.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, CarroceriaRequestDto carroceriaRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Carroceria carroceriaBack = _carroceria.GetById(id.Value);
			if (carroceriaBack is null) return NotFound();
			carroceriaBack = _mapper.Map<Carroceria>(carroceriaRequestDto);
			_carroceria.Save(carroceriaBack);
			return Ok();
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			Carroceria carroceriaBack = _carroceria.GetById(id.Value);
			if (carroceriaBack is null) return NotFound();
			_carroceria.Delete(carroceriaBack.Id);
			return Ok();
		}
	}
}
