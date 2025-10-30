using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Transmision;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransmisionesController : ControllerBase
	{
		private readonly IApplication<Transmision> _transmision;
		private readonly ILogger<TransmisionesController> _logger;
		private readonly IMapper _mapper;

		public TransmisionesController(IApplication<Transmision> transmision,
									   ILogger<TransmisionesController> logger,
									   IMapper mapper)
		{
			_transmision = transmision;
			_logger = logger;
			_mapper = mapper;
		}
		[HttpGet]
		[Route("All")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IList<TransmisionResponseDto>>(_transmision.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> GetById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			Transmision transmision = _transmision.GetById(id.Value);
			if (transmision is null) return NotFound();
			return Ok(_mapper.Map<TransmisionResponseDto>(transmision));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TransmisionRequestDto transmisionRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var transmision = _mapper.Map<Transmision>(transmisionRequestDto);
			_transmision.Save(transmision);
			return Ok(transmision.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TransmisionRequestDto transmisionRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			Transmision transmisionBack = _transmision.GetById(id.Value);
			if (transmisionBack is null) return NotFound();
			transmisionBack = _mapper.Map<Transmision>(transmisionRequestDto);
			_transmision.Save(transmisionBack);
			return Ok(_mapper.Map<TransmisionResponseDto>(transmisionBack));
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			var transmisionBack = _transmision.GetById(id.Value);
			if (transmisionBack is null) return NotFound();
			_transmision.Delete(transmisionBack.Id);
			return Ok();
		}
	}
}
