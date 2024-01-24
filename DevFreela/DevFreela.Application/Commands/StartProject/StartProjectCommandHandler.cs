using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;
        // private readonly string _connectionString;

        public StartProjectCommandHandler(DevFreelaDbContext dbContext/*, IConfiguration configuration*/)
        {
            _dbContext = dbContext;
            // _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
            project.Start();
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        /*
        // With dapper
        public async Task StartWithDapperAsync(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                string script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = DateTime.Now, id });
            }
        }*/
    }
}