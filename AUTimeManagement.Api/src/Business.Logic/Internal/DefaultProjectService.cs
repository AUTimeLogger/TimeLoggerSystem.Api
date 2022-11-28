using AUTimeManagement.Api.Business.Logic.Models;
using AUTimeManagement.Api.Business.Logic.Services;
using AUTimeManagement.Api.DataAccess.Layer.Service;
using AutoMapper;

namespace AUTimeManagement.Api.Business.Logic.Internal
{
    internal class DefaultProjectService : IProjectService
    {
        private readonly IDataReader reader;
        private readonly IDataWriter writer;
        private readonly IMapper mapper;

        public DefaultProjectService(IDataReader reader, IDataWriter writer, IMapper mapper)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
            this.mapper = mapper;
        }

        public async Task<Guid> AddProject(string projectName)
        {
            return await writer.AddProject(projectName);
        }

        public async Task AddUser(Guid projectId, Guid userId)
        {
            await writer.AssignUser(projectId, userId);
        }

        public async Task AddWork(Guid projectid, Guid userId, CreateWorkModel work)
        {
            var model = mapper.Map<DataAccess.Layer.Model.CreateWorkModel>(work);

            await writer.AddWork(projectid, userId, model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await writer.DeleteProject(id);
        }

        public async Task<ProjectModel?> FindAsync(Guid id)
        {
            var x = await reader.GetProject(id);
            if (x == null)
            {
                return null;
            }

            var project = mapper.Map<ProjectModel>(x);

            return project;
        }

        public async Task<ICollection<ProjectModel>> GetAsync()
        {
            var list = await reader.GetProjects();

            if (list == null)
            {
                return new List<ProjectModel>();
            }

            var models = mapper.Map<ICollection<ProjectModel>>(list);

            return models;
        }

        public async Task UpdateAsync(Guid id, UpdateProjectModel model)
        {
            await writer.UpdateProject(id, model.ProjectName);
        }
    }
}
