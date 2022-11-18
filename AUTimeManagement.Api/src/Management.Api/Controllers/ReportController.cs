using Microsoft.AspNetCore.Mvc;

namespace AUTimeManagement.Api.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        /// <summary>
        /// Endpoint GET /api/report/
        /// 
        /// <br/>
        /// Return all users, all projects
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Produces("text/csv")]
        public Task<IActionResult> OnGet(
            [FromQuery] int year)
        {
            var x = new { projectId = "", workHours = 1, userName = "Feri", email="" };
            throw new NotImplementedException();
        }
    }
}
