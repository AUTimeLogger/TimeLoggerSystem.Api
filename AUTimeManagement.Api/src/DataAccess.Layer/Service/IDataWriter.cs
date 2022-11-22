using AUTimeManagement.Api.DataAccess.Layer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.DataAccess.Layer.Service
{
    public interface IDataWriter
    {
        Task<Guid> AddDepartment(string departmentName);
        Task<Guid> AddProject(string projectName);
        Task<Guid> AddWork(Guid projectId, Guid userId, CreateWorkModel data);
        Task<Guid> AssignUser(Guid projectId, Guid userId);
    }
}
