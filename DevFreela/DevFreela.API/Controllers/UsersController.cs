using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.Login;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Queries.GetUser;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string query)
        {
            GetAllUsersQuery getAllUsersQuery = new GetAllUsersQuery(query);
            List<UserViewModel> users = await _mediator.Send(getAllUsersQuery);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            GetUserByIdQuery query = new GetUserByIdQuery(id);
            UserDetailsViewModel user = await _mediator.Send(query);
            
            if(user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdAsync), new { id }, command);
        }

        [HttpPut("{id}/login")]
        public async Task<IActionResult> LoginAsync(int id, [FromBody] LoginCommandHandler command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
