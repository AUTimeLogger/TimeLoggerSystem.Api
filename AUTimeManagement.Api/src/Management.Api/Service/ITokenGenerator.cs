using AUTimeManagement.Api.Management.Api.Security.Model;

namespace AUTimeManagement.Api.Management.Api.Service
{
    public interface ITokenGenerator
    {
        Task<string> GetToken(ApplicationUser user);
    }
}
