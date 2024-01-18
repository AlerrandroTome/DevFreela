using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public void Finish(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            project.Finish();
            _dbContext.SaveChanges();
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            Project project = _dbContext.Projects.Include(p => p.Client)
                                                 .Include(p => p.Freelancer)
                                                 .SingleOrDefault(p => p.Id == id);
            
            if(project is null)
            {
                return null;
            }

            ProjectDetailsViewModel projectDetailsViewModel = new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinisedAt, project.Client.FullName, project.Freelancer.FullName);
            return projectDetailsViewModel;
        }

        public void Start(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            project.Start();
            _dbContext.SaveChanges();
        }

        public async Task StartWithDapperAsync(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                string script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = DateTime.Now, id});
            }
        }
    }
}
