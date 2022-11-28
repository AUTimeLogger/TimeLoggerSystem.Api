using AUTimeManagement.Api.Business.Logic.Services;

namespace AUTimeManagement.Api.Business.Logic.Internal;

internal class DefaultBusinessService : IBusinessService
{
    public DefaultBusinessService(IProjectService projects, IReportService report)
    {
        Projects = projects ?? throw new ArgumentNullException(nameof(projects));
        Report = report ?? throw new ArgumentNullException(nameof(report));
    }

    public IProjectService Projects { get; internal set; }

    public IReportService Report { get; internal set; }
}
