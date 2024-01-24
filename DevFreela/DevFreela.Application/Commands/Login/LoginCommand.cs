using MediatR;

namespace DevFreela.Application.Commands.Login
{
    public class LoginCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
