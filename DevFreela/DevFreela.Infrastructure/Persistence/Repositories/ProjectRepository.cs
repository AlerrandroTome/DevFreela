using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            Project project = await _dbContext.Projects.Include(p => p.Client)
                                                 .Include(p => p.Freelancer)
                                                 .SingleOrDefaultAsync(p => p.Id == id);
            return project;
        }

        public async Task AddAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            project.Cancel();
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _dbContext.Update(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartAsync(int id)
        {
            Project project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
            project.Start();
            await _dbContext.SaveChangesAsync();
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

        public async Task FinishAsync(int id)
        {
            Project project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
            project.Finish();
            await _dbContext.SaveChangesAsync();
        }

        public async Task SetPaymentPendind(int id)
        {
            Project project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
            project.SetPaymentPending();
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
