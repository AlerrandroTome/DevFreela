using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetUser
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsViewModel>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDetailsViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                return null;
            }

            UserDetailsViewModel userDetailsViewModel = new UserDetailsViewModel(user.Id, user.FullName, user.Email, user.Username, user.BirthDate, user.CreatedAt, user.Active);
            return userDetailsViewModel;
        }
    }
}
