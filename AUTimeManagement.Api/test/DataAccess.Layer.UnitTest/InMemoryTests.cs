using AUTimeManagement.Api.DataAccess.Layer;
using AUTimeManagement.Api.DataAccess.Layer.Context;
using DataAccess.Layer.UnitTest.Scenarios;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.UnitTest;

public class InMemoryTests
{
    public class RawContextTests
    {
        [Fact]
        public async Task Test1()
        {
            DataContext context = GetContext;
            await context.Database.EnsureCreatedAsync();

            var time = DateTime.UtcNow;
            var expected = new AUTimeManagement.Api.DataAccess.Layer.Model.Internal.Project { ProjectName = "Test Project", Updated = time, Created = time };

            await context.Projects.AddAsync(expected);

            await context.SaveChangesAsync();

            var project = context.Projects.First();
            
            Assert.NotNull(project);

            Assert.Equal(project.Id, expected.Id);
        }



        [Fact]
        public async Task Test2()
        {
            DataContext context = GetContext;

            await context.Database.EnsureCreatedAsync();

            var time = DateTime.UtcNow;
            var expected = new AUTimeManagement.Api.DataAccess.Layer.Model.Internal.Project { ProjectName = "Test Project", Updated = time, Created = time };

            await context.Projects.AddAsync(expected);

            await context.SaveChangesAsync();

            var project = context.Projects.First();

            var model = new AUTimeManagement.Api.DataAccess.Layer.Model.Internal.ProjectToUser { UserId = Guid.NewGuid() };
            project.Users.Add(model);
            await context.SaveChangesAsync();

            var actual = context.ProjectAssociation.First();

            Assert.Equal(model.UserId, actual.UserId);
            Assert.Equal(expected.Id, actual.ProjectId);
        }

        [Fact]
        public async Task Test3()
        {
            DataContext context = GetContext;
            await context.Database.EnsureCreatedAsync();

            var time = DateTime.UtcNow;
            var expected = new AUTimeManagement.Api.DataAccess.Layer.Model.Internal.Department { DepartmentName = "TestDepartment", DepartmentDirector = Guid.NewGuid(), Created = time, Updated = time };

            context.Departments.Add(expected);

            await context.SaveChangesAsync();

            var actual = context.Departments.First();

            Assert.NotNull(actual);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.DepartmentName, actual.DepartmentName);
            Assert.Equal(expected.DepartmentDirector, actual.DepartmentDirector);
        }

        private IOptions<DataAccessOptions> GetOptions => 
            Options.Create(new DataAccessOptions { ConnectionString = Guid.NewGuid().ToString(), Database = DataAccessOptions.DatabaseType.InMemory });

        private DataContext GetContext =>
            new DataContext(GetOptions);
        
    }

    public class InjectionTests
    {
        [Fact]
        public async Task Test1()
        {
            var scenario = new InjectionScenario();

            var expectedName = "random department";

            var guid = await scenario.Writer.AddProject(expectedName);

            var actual = await scenario.Reader.GetProject(guid);

            Assert.NotNull(actual);

            Assert.Equal(expectedName, actual.ProjectName);
            Assert.Equal(guid, actual.Id); 
        }

        [Fact]
        public async Task Test2()
        {
            var scenario = new InjectionScenario();

            var projects = await scenario.Reader.GetProjects();

            Assert.NotNull(projects);


            var projectId = projects.First().Id;

            var userId = Guid.NewGuid();
            await scenario.Writer.AssignUser(projectId, userId);

            var actual = await scenario.Reader.GetUsers(projectId);

            Assert.Contains(userId, actual);
        }
    }

}
