using AUTimeManagement.Api.DataAccess.Layer.Context;
using AUTimeManagement.Api.DataAccess.Layer.Model;
using AUTimeManagement.Api.DataAccess.Layer.Model.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.DataAccess.Layer.Service;

internal sealed class DefaultDataWriter : IDataWriter
{
    private readonly DataContext context;

    public DefaultDataWriter(DataContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Guid> AddDepartment(string departmentName)
    {
        var model = new Model.Internal.Department { DepartmentName = departmentName, DepartmentDirector = Guid.NewGuid() };
        context.Departments.Add(model);


        await SaveChanges().ConfigureAwait(false);

        return model.Id;
    }

    public async Task<Guid> AddProject(string projectName)
    {
        var model = new Model.Internal.Project { ProjectName = projectName };
        context.Projects.Add(model);

        await SaveChanges().ConfigureAwait(false);

        return model.Id;
    }

    public async Task<Guid> AddWork(Guid projectId, Guid userId, CreateWorkModel data)
    {
        var project = await context.Projects.FindAsync(projectId);

        if (project == null) throw new Exception();

        var model = data.ToWorkUnit();

        model.Project = project;
        model.UserId = userId;

        project.WorkUnits.Add(model);

        await SaveChanges().ConfigureAwait(false);

        return model.Id;
    }

    public async Task<Guid> AssignUser(Guid projectId, Guid userId)
    {
        var project = await context.Projects.FindAsync(projectId);

        if (project == null) throw new Exception();

        var model = new Model.Internal.ProjectToUser { UserId = userId };

        project.Users.Add(model);

        await SaveChanges().ConfigureAwait(false);


        return projectId;
    }

    private async Task SaveChanges()
    {
        var enties = context.ChangeTracker.Entries().Where(e=>e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in enties)
        {
            if(entry.Entity is DataModelBase)
            {
                var e = (DataModelBase)entry.Entity;    var time = DateTime.UtcNow;
                if(entry.State == EntityState.Added)
                {
                    e.Created = time;
                }

                e.Updated = time;
            }
        }
        await context.SaveChangesAsync();
    }
}
