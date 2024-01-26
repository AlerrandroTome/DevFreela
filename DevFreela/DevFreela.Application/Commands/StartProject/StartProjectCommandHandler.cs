using DevFreela.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;
        // private readonly string _connectionString;

        public StartProjectCommandHandler(IProjectRepository projectRepository/*, IConfiguration configuration*/)
        {
            _projectRepository = projectRepository;
            // _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }


        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectRepository.StartAsync(request.Id);
            return Unit.Value;
        }
    }
}