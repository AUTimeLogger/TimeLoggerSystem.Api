using AUTimeManagement.Api.Business.Logic.Models;
using AUTimeManagement.Api.Business.Logic.Services;
using AUTimeManagement.Api.Management.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AUTimeManagement.Api.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IBusinessService _service;
        private readonly IMapper mapper;

        public ProjectsController(IBusinessService service, IMapper mapper)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProjectViewModel>>> OnGet()
        {
            var projects = await _service.Projects.GetAsync();

            var x = mapper.Map<ICollection<ProjectViewModel>>(projects);

            return x.ToList();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnCreate([FromBody] CreateProjectViewModel model)
        {
            var id = await _service.Projects.AddProject(model.ProjectName);

            return CreatedAtRoute(nameof(OnGet), new { projectId = id });
        }

        [Route("{projectId:guid}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectViewModel>> OnGet([FromRoute] Guid projectId)
        {
            var project = await _service.Projects.FindAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            var x = mapper.Map<ProjectViewModel>(project);

            return x;
        }

        [Route("{projectId:guid}")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OnUpdate(
            [FromRoute] Guid projectId,
            [FromBody] UpdateProjectViewModel model)
        {
            var proj = mapper.Map<UpdateProjectModel>(model);
            await _service.Projects.UpdateAsync(projectId, proj);

            return NoContent();
        }

        [Route("{projectId:guid}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> OnDelete([FromRoute] Guid projectId)
        {
            await _service.Projects.DeleteAsync(projectId);

            return NoContent();
        }
    }
}
