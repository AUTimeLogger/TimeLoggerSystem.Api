using AUTimeManagement.Api.DataAccess.Layer.Model;

namespace AUTimeManagement.Api.DataAccess.Layer.Service;

public interface IDataWriter
{
    Task<Guid> AddDepartment(string departmentName);
    Task<Guid> AddProject(string projectName);
    Task<Guid> AddWork(Guid projectId, Guid userId, CreateWorkModel data);
    Task<Guid> AssignUser(Guid projectId, Guid userId);
    Task DeleteProject(Guid projectId);
    Task UpdateProject(Guid projectId, string projectName);
}
