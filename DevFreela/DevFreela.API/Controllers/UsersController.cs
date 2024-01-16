using DevFreela.API.Models;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            List<UserViewModel> users = _userService.GetAll(query);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UserDetailsViewModel user = _userService.GetById(id);
            
            if(user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewUserInputModel createUserModel)
        {
            int id = _userService.Create(createUserModel);
            return CreatedAtAction(nameof(GetById), new { id }, createUserModel);
        }

        [HttpPut("{id}/login")]
        public IActionResult Login(int id, [FromBody] LoginModel loginModel)
        {
            return NoContent();
        }
    }
}
