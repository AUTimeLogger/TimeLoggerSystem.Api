using AUTimeManagement.Api.Management.Api.Security.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AUTimeManagement.Api.Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly UserManager<ApplicationUser> userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<UserViewModel>>> OnGet()
    {
        var users = await userManager.Users.ToListAsync();
        List<UserViewModel> result = new List<UserViewModel>();
        users.ForEach(async u => result.Add(new UserViewModel(u.UserName, u.Email, "", "", await userManager.IsInRoleAsync(u, "Admin"))));

        return result;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> OnPost([FromBody] CreateUserModel model)
    {
        var pw = model.Password.Trim();

        var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
        var result = await userManager.CreateAsync(user, pw);


        return CreatedAtAction(nameof(OnGet), new { userId = Guid.NewGuid() });
    }

    [Route("{userId:guid}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserViewModel>> OnGet([FromRoute] Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return NotFound();
        }
        var model = new UserViewModel(user.UserName, "", "", user.Email, await userManager.IsInRoleAsync(user, "Admin"));
        return model;
    }

    [Route("{userId:guid}")]
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult OnUpdate([FromRoute] Guid userId)
    {
        return NoContent();
    }

    [Route("{userId:guid}")]
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult OnDelete([FromRoute] Guid userId)
    {
        return NoContent();
    }

    [Route("/api/me")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserViewModel>> OnMe()
    {
        var user = await userManager.GetUserAsync(User);

        if (user is null)
        {
            return Forbid();
        }

        var userModel = new UserViewModel(user.UserName, "", "", user.Email, await userManager.IsInRoleAsync(user, "Admin"));

        return userModel;
    }
}
