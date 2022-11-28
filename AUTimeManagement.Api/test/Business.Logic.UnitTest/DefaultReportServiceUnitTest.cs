using AUTimeManagement.Api.Business.Logic.Internal;
using AUTimeManagement.Api.DataAccess.Layer.Model;
using Business.Logic.UnitTest.MockedClasses;
namespace Business.Logic.UnitTest;

public class DefaultReportServiceUnitTest
{
    [Fact]
    public async Task Test1()
    {
        Guid userId = Guid.NewGuid();
        Guid projectId = Guid.NewGuid();
        List<ProjectData> data = new()
        {
            new ProjectData
            {
                Id = projectId,
                ProjectName = "Project1",
                Users = new(){ userId },
                Works = new List<Work>()
                {
                    new Work
                    {
                        Comment="ASd",
                        ProjectId=projectId,
                        UserId = userId,
                        WorkDate=DateTime.Now,
                        EndTime=DateTime.Now,
                        StartTime=DateTime.Now,
                        Duration=480,
                    }
                }
            }
        };
        MockedClasses.MockDataReader reader = new MockedClasses.MockDataReader(data);
        DefaultReportService service = new DefaultReportService(reader);

        var actual = await service.CreateReportAsync(DateTime.Now.Year);

        Assert.NotNull(actual);

        Assert.StartsWith(Work.GetHeader("csv"), actual);

    }
}
