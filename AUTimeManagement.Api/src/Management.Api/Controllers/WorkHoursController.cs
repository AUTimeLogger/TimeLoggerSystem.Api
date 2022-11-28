using AUTimeManagement.Api.Business.Logic.Models;
using AUTimeManagement.Api.Business.Logic.Services;
using AUTimeManagement.Api.Management.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AUTimeManagement.Api.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkHoursController : ControllerBase
    {
        private readonly IBusinessService _service;
        private readonly IMapper mapper;

        public WorkHoursController(IBusinessService service, IMapper mapper)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> OnCreate(
            [FromQuery] Guid userId,
            [FromQuery] Guid projectId,
            [FromBody] WorkUnitViewModel model)
        {
            //TODO validate userId

            try
            {
                var work = mapper.Map<CreateWorkModel>(model);
                await _service.Projects.AddWork(projectId, userId, work);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
