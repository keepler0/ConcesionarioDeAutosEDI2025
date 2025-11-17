using AutoMapper;
using Concesionario.Application.Dtos.Identity.Roles;
using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, 
                               ILogger<RolesController> logger, 
                               IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("Get All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<RoleResponseDto>>(_roleManager.Roles.ToList()));
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Guardar(RoleRequestDto roleRequestDto)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("id")?.Value);
                try
                {
                    var role = _mapper.Map<Role>(roleRequestDto);
                    var result = _roleManager.CreateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description,
                                   instance: role.Name,
                                   StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos");
            }
        }
        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] RoleRequestDto roleRequestDto, [FromQuery] Guid id)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                try
                {
                    var role = _mapper.Map<Role>(roleRequestDto);
                    role.Id = id;
                    var result = _roleManager.UpdateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description,
                                   instance: role.Name,
                                   StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos");
            }
        }
        [HttpGet]
        [Route("Obtener por Id")]
        public IActionResult GetById(Guid? id)
        {
            try
            {
                if (!id.HasValue) return BadRequest();
                var roleBack = _roleManager.FindByIdAsync(id.Value.ToString());
                if (roleBack is null) return NotFound();
                return Ok(_mapper.Map<RoleResponseDto>(roleBack));
                //TODO: preguntar sobre error en el automaper ya que pierde el mapeo
            }
            catch (Exception ex)
            {
                return Conflict();
            }
        }
    }
}
