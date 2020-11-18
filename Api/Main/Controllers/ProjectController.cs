using System.Collections.Generic;
using Catalyst.Api.Main.Models;
using Catalyst.Api.Main.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalyst.Api.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService) => _projectService = projectService;

        [HttpGet]
        public ActionResult<IEnumerable<Project>> Get()
        {
            return Ok(_projectService.FetchAll());
        }

        [HttpGet("{id:long}")]
        public ActionResult<Project> GetSingle(long id)
        {
            Project project = _projectService.FetchSingle(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public ActionResult<Project> Create(Project project)
        {
            _projectService.Create(project);

            return CreatedAtAction(nameof(GetSingle), new {id = project.Id}, project);
        }

        [HttpPut("{id:long}")]
        public ActionResult Update(long id, Project project)
        {
            if (!_projectService.Exists(id))
            {
                return NotFound();
            }

            _projectService.Update(project);

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            if (!_projectService.Exists(id))
            {
                return NotFound();
            }

            _projectService.Remove(id);

            return NoContent();
        }
    }
}