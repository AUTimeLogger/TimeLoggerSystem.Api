using AUTimeManagement.Api.DataAccess.Layer;
using AUTimeManagement.Api.DataAccess.Layer.Context;
using AUTimeManagement.Api.DataAccess.Layer.Model.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Layer.UnitTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddDataAccessLayer(x =>
        {
            x.Database = DataAccessOptions.DatabaseType.InMemory;
            x.ConnectionString = "TestDb";
        });

        var provider = services.BuildServiceProvider();

        using var context = provider.GetRequiredService<DataContext>();
        context.Database.EnsureDeleted();
        var x = context.Database.ProviderName;

        Assert.NotNull(x);

        var result = context.Database.EnsureCreated();

        Assert.True(result);

        var t = DateTime.UtcNow;
        var department = new Department { DepartmentName = "test departmetn", DepartmentDirector = Guid.NewGuid(), Created = t, Updated = t };
        context.Departments.Add(department);

        context.SaveChanges();
    }
}