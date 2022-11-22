using AUTimeManagement.Api.DataAccess.Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.DataAccess.Layer.Service;

public interface IDataReader
{
    Task<ICollection<Project>> GetProjects();
    Task<Project?> GetProject(Guid id);
    Task<ICollection<Work>> GetWorks(
        Guid projectId, 
        Guid? userId = null
    );
    Task<ICollection<Guid>> GetUsers(Guid projectId);

}
