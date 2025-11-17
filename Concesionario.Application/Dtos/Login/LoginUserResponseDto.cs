namespace Concesionario.Application.Dtos.Login
{
	public class LoginUserResponseDto
	{
        public string Token { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
		public List<string> Errores { get; set; }
        public bool Login { get; set; }
    }
}
