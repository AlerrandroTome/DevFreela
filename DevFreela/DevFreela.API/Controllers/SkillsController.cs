using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevFreela.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<SkillViewModel> skills = _skillService.GetAll();
            return Ok(skills);
        }
    }
}
