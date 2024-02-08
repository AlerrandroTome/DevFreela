using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Role = request.Role.ToLower();
            string passwordHash = _authService.ComputeSha256Hash(request.Password);
            User user = new User(request.FullName, request.Email, request.Username, passwordHash, request.Role, request.BirthDate);
            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}