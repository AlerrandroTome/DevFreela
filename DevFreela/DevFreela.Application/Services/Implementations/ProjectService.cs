using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewProjectInputModel inputModel)
        {
            Project project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            ProjectComment project = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.ProjectComments.Add(project);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            project.Cancel();
            _dbContext.SaveChanges();
        }

        public void Finish(int id)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
            project.Finish();
            _dbContext.SaveChanges();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            List<Project> projects = _dbContext.Projects.ToList();
            List<ProjectViewModel> projectsViewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                                                               .ToList();
            return projectsViewModel;
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

        public void Update(UpdateProjectInputModel inputModel)
        {
            Project project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
            _dbContext.SaveChanges();
        }
    }
}
