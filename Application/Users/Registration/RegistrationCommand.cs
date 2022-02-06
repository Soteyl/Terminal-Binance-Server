using MediatR;

namespace Ixcent.CryptoTerminal.Application.Users.Registration
{
	public class RegistrationCommand : IRequest<User>
	{
		public string UserName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;
	}
}