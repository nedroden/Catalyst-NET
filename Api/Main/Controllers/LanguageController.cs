using System.Collections.Generic;
using Catalyst.Api.Main.Models;
using Catalyst.Api.Main.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalyst.Api.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageService _languageService;
        private readonly ProjectService _projectService;

        public LanguageController(LanguageService languageService, ProjectService projectService)
        {
            _languageService = languageService;
            _projectService = projectService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Language>> Get()
        {
            return Ok(_languageService.FetchAll());
        }

        [HttpGet("{id:long}")]
        public ActionResult<Language> GetSingle(long id)
        {
            Language language = _languageService.FetchSingle(id);

            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        [HttpPost]
        public ActionResult<Language> Create(Language language)
        {
            if (!_projectService.Exists(language.ProjectId))
            {
                return NotFound(new { error = "Project not found." });
            }

            try
            {
                _languageService.Create(language);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500);
            }

            return CreatedAtAction(nameof(GetSingle), new { id = language.Id }, language);
        }
    }
}