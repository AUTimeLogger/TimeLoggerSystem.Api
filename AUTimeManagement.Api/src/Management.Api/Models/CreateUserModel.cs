namespace AUTimeManagement.Api.Management.Api.Models;

public record CreateUserModel(string UserName, string Password, string Email, bool IsAdmin);
