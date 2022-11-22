using AUTimeManagement.Api.Management.Api.Security.Model;
using AUTimeManagement.Api.Management.Api.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AUTimeManagement.Api.Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessController : ControllerBase
{
    private readonly UserManager<ApplicationUser> mg;
    private readonly ITokenGenerator tokenGenerator;

    public AccessController(UserManager<ApplicationUser> mg, ITokenGenerator tokenGenerator)
    {
        this.mg = mg ?? throw new ArgumentNullException(nameof(mg));
        this.tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
    }

    [AllowAnonymous]
    [Route("login")]
    [HttpPost]
    [ProducesErrorResponseType(typeof(BadRequestResult))]
    [Produces(typeof(LoginResponse))]
    public async Task<IActionResult> OnLogin([FromBody] LoginRequest login)
    {
        var user = await mg.FindByNameAsync(login.Username);
        if(user is not null)
        {
            if(await mg.CheckPasswordAsync(user, login.Password))
            {
                var token = await tokenGenerator.GetToken(user);
                await mg.SetAuthenticationTokenAsync(user, "DefaultJWTProvider", "jwt", token).ConfigureAwait(false);
                return Ok(new LoginResponse(token));
            }
        }
        return BadRequest();
    }

    [Route("logout")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> OnLogout()
    {
        var user = await mg.GetUserAsync(User);
        await mg.RemoveAuthenticationTokenAsync(user, "DefaultJWTProvider", "jwt").ConfigureAwait(false);

        return NoContent();
    }
}
