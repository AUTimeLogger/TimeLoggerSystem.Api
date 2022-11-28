using AUTimeManagement.Api.Business.Logic.Models;
using CreateWorkModel = AUTimeManagement.Api.Business.Logic.Models.CreateWorkModel;

namespace AUTimeManagement.Api.Business.Logic.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// Finds a project based on given id.
        /// </summary>
        /// <param name="id">Id of the project</param>
        /// <returns>A project type</returns>
        Task<ProjectModel?> FindAsync(Guid id);

        /// <summary>
        /// Returns all projects in DB.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<ProjectModel>> GetAsync();

        Task<Guid> AddProject(string projectName);

        Task AddUser(Guid projectId, Guid userId);

        /// <summary>
        /// Updates a project
        /// </summary>
        /// <param name="id">Id of a project</param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, UpdateProjectModel model);

        /// <summary>
        /// Deletes a project
        /// </summary>
        /// <param name="id">Id of a project</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Adds a work unit to a project.
        /// </summary>
        /// <param name="id">Id of a project</param>
        /// <param name="work"> work unit</param>
        /// <returns></returns>
        Task AddWork(Guid projectid, Guid userId, CreateWorkModel work);
    }
}
