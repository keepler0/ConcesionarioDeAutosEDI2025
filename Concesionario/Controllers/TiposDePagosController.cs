using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.Marca;
using Concesionario.Application.Dtos.TipoDePago;
using Concesionario.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TiposDePagosController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IApplication<TipoDePago> _tipoDePago;
		private readonly ILogger<TiposDePagosController> _logger;

		public TiposDePagosController(IMapper mapper, 
									  IApplication<TipoDePago> tipoDePago, 
									  ILogger<TiposDePagosController> logger)
		{
			_mapper = mapper;
			_tipoDePago = tipoDePago;
			_logger = logger;
		}
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<TipoDePagoResponseDto>>(_tipoDePago.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			TipoDePago tipoDePago = _tipoDePago.GetById(id.Value);
			if (tipoDePago is null) return NotFound();
			return Ok(_mapper.Map<TipoDePagoResponseDto>(tipoDePago));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(TipoDePagoRequestDto tipoDePagoRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var tipoDePago = _mapper.Map<TipoDePago>(tipoDePagoRequestDto);
			_tipoDePago.Save(tipoDePago);
			return Ok(tipoDePago.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, TipoDePagoRequestDto tipoDePagoRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			TipoDePago tipoDePagoBack = _tipoDePago.GetById(id.Value);
			if (tipoDePagoBack is null) return NotFound();
			tipoDePagoBack = _mapper.Map<TipoDePago>(tipoDePagoRequestDto);
			_tipoDePago.Save(tipoDePagoBack);
			return Ok();
		}
		[HttpDelete]
		[Route("Delete")]
		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			TipoDePago tipoDePagoBack = _tipoDePago.GetById(id.Value);
			if (tipoDePagoBack is null) return NotFound();
			_tipoDePago.Delete(tipoDePagoBack.Id);
			return Ok();
		}
	}
}
