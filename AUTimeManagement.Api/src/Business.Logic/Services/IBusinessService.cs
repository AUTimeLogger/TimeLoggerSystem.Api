namespace AUTimeManagement.Api.Business.Logic.Services;

public interface IBusinessService
{
    IProjectService Projects { get; }
    IReportService Report { get; }
}
