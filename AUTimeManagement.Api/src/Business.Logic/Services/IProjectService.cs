using AUTimeManagement.Api.DataAccess.Layer.Model;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.Business.Logic.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// Finds a project based on given id.
        /// </summary>
        /// <param name="id">Id of the project</param>
        /// <returns>A project type</returns>
        Task FindAsync(object id);
        
        /// <summary>
        /// Returns all projects in DB.
        /// </summary>
        /// <returns></returns>
        Task GetAsync();

        /// <summary>
        /// Updates a project
        /// </summary>
        /// <param name="id">Id of a project</param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateAsync(object id, object model);

        /// <summary>
        /// Deletes a project
        /// </summary>
        /// <param name="id">Id of a project</param>
        /// <returns></returns>
        Task DeleteAsync(object id);

        /// <summary>
        /// Adds a work unit to a project.
        /// </summary>
        /// <param name="id">Id of a project</param>
        /// <param name="work"> work unit</param>
        /// <returns></returns>
        Task AddWork(object id, Work work);
    }
}
