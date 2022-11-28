using AUTimeManagement.Api.Business.Logic.Services;
using AUTimeManagement.Api.DataAccess.Layer.Model;
using AUTimeManagement.Api.DataAccess.Layer.Service;
using System.Text;

namespace AUTimeManagement.Api.Business.Logic.Internal;

internal sealed class DefaultReportService : IReportService
{
    private readonly IDataReader _reader;

    public DefaultReportService(IDataReader reader)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    }

    public async Task<string> CreateReportAsync(int year = 2022)
    {
        StringBuilder builder = Builder;

        var projects = await _reader.GetProjects();

        foreach (var p in projects)
        {
            builder = await CreateReportAsync(year, p.Id, builder);
        }

        return builder.ToString().Trim();
    }

    public async Task<string> CreateReportAsync(int year, Guid projectId)
    {
        StringBuilder builder = Builder;

        builder = await CreateReportAsync(year, projectId, builder);

        return builder.ToString().Trim();
    }

    public async Task<string> CreateReportAsync(int year, Guid projectId, string userId)
    {
        StringBuilder builder = Builder;

        builder = await CreateReportAsync(year, projectId, userId, builder);

        return builder.ToString().Trim();
    }

    private async Task<StringBuilder> CreateReportAsync(int year, Guid projectId, StringBuilder sb)
    {
        var users = await _reader.GetUsers(projectId);

        foreach (var user in users)
        {
            sb = await CreateReportAsync(year, projectId, user.ToString(), sb);
        }

        return sb;
    }

    private async Task<StringBuilder> CreateReportAsync(int year, Guid projectId, string userId, StringBuilder sb)
    {
        var works = from w in await _reader.GetWorks(projectId, Guid.Parse(userId))
                    where w.WorkDate.Year == year
                    select w;

        foreach (var work in works)
        {
            sb.AppendLine(work.ToString("csv"));
        }

        return sb;
    }

    private StringBuilder Builder
    {
        get
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Work.GetHeader("csv"));

            return builder;
        }
    }
}
