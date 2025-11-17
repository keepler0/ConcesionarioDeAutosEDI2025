using AutoMapper;
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
	public class UsersController : ControllerBase
	{
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;
		private	readonly ILogger<UsersController> _logger;
		private readonly IMapper _mapper;

		public UsersController(RoleManager<Role> roleManager, 
							   UserManager<User> userManager, 
							   ILogger<UsersController> logger, 
							   IMapper mapper)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_logger = logger;
			_mapper = mapper;
		}
		[HttpPost]
		[Route("AgregarRolAUsuario")]
		public async Task<IActionResult> Guardar(string? userId,string? roleId)
		{
			var role = _roleManager.FindByIdAsync(roleId!).Result;
			var user = _userManager.FindByIdAsync(userId!).Result;

			if(user is not null && role is not null)
			{
				var status = await _userManager.AddToRoleAsync(user, role.Name!);
				if (status.Succeeded) return Ok(new { user = user.UserName, role = role.Name! });
			}
			return BadRequest(new { user = user!.UserName, role = role!.Name! });
		}
	}
}
