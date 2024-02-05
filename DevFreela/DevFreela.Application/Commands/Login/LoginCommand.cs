using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.Commands.Login
{
    public class LoginCommand : IRequest<UserLoginViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
