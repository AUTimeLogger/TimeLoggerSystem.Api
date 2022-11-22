using AUTimeManagement.Api.Management.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AUTimeManagement.Api.Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly UserManager<IdentityUser> userManager;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult OnGet()
    {
        return Ok();
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> OnPost([FromBody] CreateUserModel model)
    {
        var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
        var result = await userManager.CreateAsync(user, model.Password);


        return CreatedAtAction(nameof(OnGet), new { userId = Guid.NewGuid() });
    }

    [Route("{userId:guid}")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserViewModel))]
    public IActionResult OnGet([FromRoute] Guid userId)
    {
        return Ok();
    }

    [Route("{userId:guid}")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult OnUpdate([FromRoute] Guid userId)
    {
        return Ok();
    }

    [Route("{userId:guid}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult OnDelete([FromRoute] Guid userId)
    {
        return Ok();
    }

    [Route("/api/me")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserViewModel))]
    public Task<IActionResult> OnMe()
    {
        throw new NotImplementedException();
    }
}
