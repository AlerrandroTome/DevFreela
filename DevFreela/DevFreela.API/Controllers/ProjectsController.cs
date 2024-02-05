using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            GetAllProjectsQuery getAllProjectsQuery = new GetAllProjectsQuery(query);
            List<ProjectViewModel> projects = await _mediator.Send(getAllProjectsQuery);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GetProjectByIdQuery query = new GetProjectByIdQuery(id);
            ProjectDetailsViewModel project = await _mediator.Send(query);
            
            if(project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            int id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteProjectCommand command = new DeleteProjectCommand() { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment([FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/start")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> StartAsync(int id)
        {
            StartProjectCommand command = new StartProjectCommand(id);
            await _mediator.Send(id);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Finish(int id)
        {
            FinishProjectCommand command = new FinishProjectCommand(id);
            await _mediator.Send(id);
            return NoContent();
        }
    }
}
