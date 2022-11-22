using AUTimeManagement.Api.DataAccess.Layer.Context;
using AUTimeManagement.Api.DataAccess.Layer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.DataAccess.Layer.Service
{
    internal sealed class DefaultDataReader : IDataReader
    {
        private readonly DataContext context;

        public DefaultDataReader(DataContext context)
        {
            this.context = context;

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Project?> GetProject(Guid id)
        {
            var project = await context.Projects.FindAsync(id);

            if(project is null)
            {
                return null;
            }

            return new Project { Id = id, ProjectName = project.ProjectName };
        }

        public async Task<ICollection<Project>> GetProjects()
        {
            var result = new List<Project>();
            var projects = await context.Projects.ToListAsync();

            projects.ForEach(x=>result.Add(new Project { Id = x.Id, ProjectName = x.ProjectName }));


            return result;
        }

        public async Task<ICollection<Guid>> GetUsers(Guid projectId)
        {
            var result = new List<Guid>();

            var project = await context.Projects.FindAsync(projectId);

            project!.Users.ForEach(x =>
                result.Add(x.UserId));

            return result;
        }

        public async Task<ICollection<Work>> GetWorks(Guid projectId, Guid? userId = null)
        {
            var units = await context.WorkUnits.Where(x => x.ProjectId == projectId).ToListAsync();
            if (userId is not null)
            {
                units = units.Where(x => x.UserId == userId).ToList();
            }

            var result = new List<Work>();

            units.ForEach(x => result.Add(new Work { Comment = x.Comment, Duration = x.Duration, EndTime = x.EndDate, ProjectId = projectId, StartTime = x.StartDate, UserId = x.UserId, WorkDate = x.WorkDate }));

            return result;
        }
    }
}
