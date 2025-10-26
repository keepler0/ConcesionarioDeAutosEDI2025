using AutoMapper;
using Concesionario.Application;
using Concesionario.Application.Dtos.CategoriaTributaria;
using Concesionario.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriasTributariasController : ControllerBase
	{
		private readonly ILogger<CategoriasTributariasController> _logger;
		private readonly IApplication<CategoriaTributaria> _ct;
		private readonly IMapper _mapper;
		public CategoriasTributariasController(ILogger<CategoriasTributariasController> logger,
											   IApplication<CategoriaTributaria> ct,
											   IMapper mapper)
		{
			_ct = ct;
			_logger = logger;
			_mapper = mapper;
		}
		[HttpGet]
		[Route("All")]
		public async Task<IActionResult> All()
		{
			return Ok(_mapper.Map<IList<CategoriaTributariaResponseDto>>(_ct.GetAll()));
		}
		[HttpGet]
		[Route("ByID")]
		public async Task<IActionResult> ById(int? id)
		{
			if (!id.HasValue) return BadRequest();
			CategoriaTributaria ct = _ct.GetById(id.Value);
			if (ct is null) return NotFound();
			return Ok(_mapper.Map<CategoriaTributariaResponseDto>(ct));
		}
		[HttpPost]
		[Route("Crear")]
		public async Task<IActionResult> Crear(CategoriaTributariaRequestDto ctRequestDto)
		{
			if (!ModelState.IsValid) return BadRequest();
			var CT = _mapper.Map<CategoriaTributaria>(ctRequestDto);
			_ct.Save(CT);
			return Ok(CT.Id);
		}
		[HttpPut]
		[Route("Editar")]
		public async Task<IActionResult> Editar(int? id, CategoriaTributariaRequestDto ctRequestDto)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();
			CategoriaTributaria ctBack = _ct.GetById(id.Value);
			if (ctBack is null) return NotFound();
			ctBack = _mapper.Map<CategoriaTributaria>(ctRequestDto);
			_ct.Save(ctBack);
			return Ok();
		}
		[HttpDelete]
		[Route("Borrar")]

		public async Task<IActionResult> Borrar(int? id)
		{
			if (!id.HasValue) return BadRequest();
			if (!ModelState.IsValid) return BadRequest();

			CategoriaTributaria ctBack = _ct.GetById(id.Value);
			if (ctBack is null) return NotFound();
			_ct.Delete(ctBack.Id);
			return Ok();
		}
	}
}
