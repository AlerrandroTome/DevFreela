using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly DevFreelaDbContext _dbContext;

        public LoginCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            return await _dbContext.Users.AnyAsync(u => u.Active
                                            && u.Username.Equals(request.Username)
                                            && u.Password.Equals(request.Password));
        }
    }
}
