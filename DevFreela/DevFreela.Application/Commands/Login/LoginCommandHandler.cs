using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserLoginViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<UserLoginViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string passwordHash = _authService.ComputeSha256Hash(request.Password);
            User user = await _userRepository.GetByEmailAndPasswordAsync(request.Email, passwordHash);
            if (user == null) 
            { 
                return null; 
            }

            string token = _authService.GenerateJwtToken(user.Email, user.Role);

            return new UserLoginViewModel(request.Email, token);
        }
    }
}
