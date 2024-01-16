using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            List<ProjectViewModel> projects = _projectService.GetAll(query);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ProjectDetailsViewModel project = _projectService.GetById(id);
            
            if(project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel createProjectModel)
        {
            if(createProjectModel.Title.Length > 50)
            {
                return BadRequest();
            }

            int id = _projectService.Create(createProjectModel);

            return CreatedAtAction(nameof(GetById), new { id }, createProjectModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel updateProjectModel)
        {
            if (updateProjectModel.Description.Length > 200)
            {
                return BadRequest();
            }

            _projectService.Update(updateProjectModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return Ok();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment([FromBody] CreateCommentInputModel createCommentModel)
        {
            _projectService.CreateComment(createCommentModel);
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);
            return NoContent();
        }
    }
}
