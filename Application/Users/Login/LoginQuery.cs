using MediatR;

namespace Ixcent.CryptoTerminal.Application.Users.Login
{
	public class LoginQuery : IRequest<User>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
