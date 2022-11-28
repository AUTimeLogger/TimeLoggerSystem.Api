namespace AUTimeManagement.Api.Business.Logic.Services;

public interface IReportService
{
    /// <summary>
    /// Creates a report on a given year containing all projects and users.
    /// </summary>
    /// <param name="year"></param>
    /// <returns>Returns a .csv string</returns>
    Task<string> CreateReportAsync(int year = 2022);

    /// <summary>
    /// Creates a report on a given year containing all users in the given project.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="projectId"></param>
    /// <returns>Returns a .csv string</returns>
    Task<string> CreateReportAsync(int year, Guid projectId);

    /// <summary>
    /// Creates a report on a given year, given project and a given user.
    /// </summary>
    /// <param name="year"></param>
    /// <param name="projectId"></param>
    /// <param name="userId"></param>
    /// <returns>Returns a .csv string</returns>
    Task<string> CreateReportAsync(int year, Guid projectId, string userId);
}
