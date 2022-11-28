using AUTimeManagement.Api.DataAccess.Layer.Model;
using AUTimeManagement.Api.DataAccess.Layer.Service;

namespace Business.Logic.UnitTest.MockedClasses;

internal class MockDataReader : IDataReader
{
    private readonly List<ProjectData> projects;
    public MockDataReader(IEnumerable<ProjectData> data)
    {
        projects = new(data);
    }

    public Task<Project?> GetProject(Guid id)
    {
        var p = projects.Find(p => p.Id == id);

        return Task.FromResult((p as Project));
    }

    public async Task<ICollection<Project>> GetProjects()
    {
        await Task.Yield();
        List<Project> p = new();

        projects.ForEach(x => p.Add(x));

        return p;
    }

    public async Task<ICollection<Guid>> GetUsers(Guid projectId)
    {
        await Task.Yield();

        var x = projects.First(p => p.Id == projectId).Users;

        return x;
    }

    public async Task<ICollection<Work>> GetWorks(Guid projectId, Guid? userId = null)
    {
        await Task.Yield();

        var p = projects.FindAll(x => x.Id == projectId);

        List<Work> works = new();

        p.ForEach(x => works.AddRange(x.Works));

        if (userId != null)
        {
            works = works.Where(w => w.UserId == userId).ToList();
        }

        return works;
    }
}

internal class ProjectData : Project
{
    public List<Guid> Users = new();
    public List<Work> Works = new();
}
