using Concesionario.Application.Dtos.Identity.User;
using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Concesionario.WebApi.Controllers.Identity
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegisterController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly ILogger<RegisterController> _logger;

		public RegisterController(UserManager<User> userManager, 
								  ILogger<RegisterController> logger)
		{
			_userManager = userManager;
			_logger = logger;
		}
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> RegistroUsuario([FromBody]UserRegistroRequestDto UserRegDto)
		{
			if (ModelState.IsValid)
			{
				var existeUser=await _userManager.FindByEmailAsync(UserRegDto.Email);
                if (existeUser is not null)
                {
					return BadRequest($"Ya existe un usuario con el email: {UserRegDto.Email}");
                }
				var crear = await _userManager.CreateAsync(
					new User()
					{
						Email = UserRegDto.Email,
						UserName = UserRegDto.Email.Substring(0, UserRegDto.Email.IndexOf('@')),
						Nombres = UserRegDto.Nombres,
						Apellidos = UserRegDto.Apellidos,
						FechaNacimiento = UserRegDto.FechaNacimiento
					},UserRegDto.Password);
				if (crear.Succeeded)
				{
					return Ok(new UserRegistroResponseDto
					{
						NombreCompleto = string.Join(" ", UserRegDto.Nombres, UserRegDto.Apellidos),
						Email=UserRegDto.Email,
						UserName= UserRegDto.Email.Substring(0, UserRegDto.Email.IndexOf('@'))
					});
				}
				else
				{
					return BadRequest(crear.Errors.Select(e => e.Description).ToList());
				}
			}
			else
			{
				return BadRequest("Los datos proporcionados no son validos");
			}
		}
	}
}
