using Concesionario.Application.Dtos.Identity.User;
using Concesionario.Application.Dtos.Login;
using Concesionario.Entities.MicrosoftIdentity;
using Concesionario.Services.RegisterServices;
using Concesionario.WebApi.Configurations;
using Microsoft.AspNetCore.Authorization;
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
		private readonly ITokenHandlerService _servicioToken;

		public RegisterController(UserManager<User> userManager,
								  ILogger<RegisterController> logger,
								  ITokenHandlerService servicioToken)
		{
			_userManager = userManager;
			_logger = logger;
			_servicioToken = servicioToken;

		}
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> RegistroUsuario([FromBody] UserRegistroRequestDto UserRegDto)
		{
			if (ModelState.IsValid)
			{
				var existeUser = await _userManager.FindByEmailAsync(UserRegDto.Email);
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
					}
					, UserRegDto.Password);
				if (crear.Succeeded)
				{
					return Ok(new UserRegistroResponseDto
					{
						NombreCompleto = string.Join(" ", UserRegDto.Nombres, UserRegDto.Apellidos),
						Email = UserRegDto.Email,
						UserName = UserRegDto.Email.Substring(0, UserRegDto.Email.IndexOf('@'))
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
		[HttpPost]
		[Route("Login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginUserRequestDto loginUserRequestDto)
		{
			if (ModelState.IsValid)
			{
				var existeUsuario = await _userManager.FindByEmailAsync(loginUserRequestDto.Email);
				if (existeUsuario is not null)
				{
					var confirm = await _userManager.CheckPasswordAsync(existeUsuario, loginUserRequestDto.Password);
					if (confirm)
					{
						try
						{
							var parametros = new TokenParameters()
							{
								Id = existeUsuario.Id.ToString(),
								PaswordHash = existeUsuario.PasswordHash,
								UserName = existeUsuario.UserName,
								Email = existeUsuario.Email
							};
							var jwt = _servicioToken.GenerateJwtTokens(parametros);
							return Ok(new LoginUserResponseDto()
							{
								Login = true,
								Token = jwt,
								UserName = existeUsuario.UserName,
								Email = existeUsuario.Email
							});
						}
						catch (Exception)
						{
							throw;
						}
					}
				}
			}
			return BadRequest(new LoginUserResponseDto()
			{
				Login = false,
				Errores = new List<string>()
				{
					"Usuario o contraseña incorrectos"
				}
			});
		}
	}
}
