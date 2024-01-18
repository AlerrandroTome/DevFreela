using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public List<SkillViewModel> GetAll()
        {
            List<Core.Entities.Skill> skills = _dbContext.Skills.ToList();
            List<SkillViewModel> skillsViewModel = skills.Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
            return skillsViewModel;
        }

        public List<SkillViewModel> GetAllWithDapper()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string script = "SELECT id, Description FROM Skills";

                return sqlConnection.Query<SkillViewModel>(script).ToList();
            }
        }
    }
}
