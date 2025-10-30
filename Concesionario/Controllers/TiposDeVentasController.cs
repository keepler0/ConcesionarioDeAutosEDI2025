using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.TipoDePago;
using Concesionario.Application.Dtos.TipoDeVenta;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TiposDeVentasController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<TiposDeVentasController> _logger;
		private readonly IApplication<TipoDeVenta> _tipoDeVenta;

		public TiposDeVentasController(IMapper mapper,
									   ILogger<TiposDeVentasController> logger,
									   IApplication<TipoDeVenta> tipoDeVenta)
		{
			_mapper = mapper;
			_logger = logger;
			_tipoDeVenta = tipoDeVenta;
		}
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(_mapper.Map<IList<TipoDeVentaResponseDto>>(_tipoDeVenta.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult>GetById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			TipoDeVenta tipoDeVenta = _tipoDeVenta.GetById(id.Value);
			if (tipoDeVenta is null) return NotFound();
			return Ok(_mapper.Map<TipoDeVentaResponseDto>(tipoDeVenta));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TipoDeVentaRequestDto tipoDeVentaRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var tipoDeVenta = _mapper.Map<TipoDeVenta>(tipoDeVentaRequestDto);
			_tipoDeVenta.Save(tipoDeVenta);
			return Ok(tipoDeVenta.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TipoDeVentaRequestDto tipoDeVentaRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			TipoDeVenta tipoDeVentaBack = _tipoDeVenta.GetById(id.Value);
			if (tipoDeVentaBack is null) return NotFound();
			tipoDeVentaBack = _mapper.Map<TipoDeVenta>(tipoDeVentaRequestDto);
			_tipoDeVenta.Save(tipoDeVentaBack);
			return Ok(_mapper.Map<TipoDePagoResponseDto>(tipoDeVentaBack));
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			TipoDeVenta tipoDeVentaBack = _tipoDeVenta.GetById(id.Value);
			if (tipoDeVentaBack is null) return NotFound();
			_tipoDeVenta.Delete(tipoDeVentaBack.Id);
			return Ok();
		}
	}
}
