using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            AssemblyInformationalVersionAttribute attribute = entryAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            return Ok(new { Version = attribute.InformationalVersion.ToString() });
        }
    }
}
