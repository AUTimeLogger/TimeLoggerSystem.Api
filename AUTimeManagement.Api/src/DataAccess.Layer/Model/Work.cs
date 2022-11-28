using AUTimeManagement.Api.DataAccess.Layer.Model.Internal;

namespace AUTimeManagement.Api.DataAccess.Layer.Model;

public class Work
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Duration { get; set; }
    public string Comment { get; set; }
    public DateTime WorkDate { get; set; }

    public static string? GetHeader(string type)
    {
        switch (type)
        {
            case "csv":
                return "ProjectId;UserId;StartTime;EndTime;Duration;Comment;WorkDate";
            default:
                throw new InvalidOperationException();
        }
    }

    public override string? ToString()
    {
        return ToString("csv");
    }

    public string? ToString(string type)
    {
        switch (type)
        {
            case "csv":
                return ToCsv();
            default:
                throw new InvalidOperationException();
        }
    }

    private string ToCsv()
    {
        return $"{ProjectId};{UserId};{StartTime};{EndTime};{Duration};{Comment};{WorkDate}";
    }
}

internal static class WorkExtensions
{
    public static WorkUnit ToWorkUnit(this Work model)
    {
        var workUnit = new WorkUnit
        {
            Comment = model.Comment,
            Duration = model.Duration,
            EndDate = model.EndTime,
            StartDate = model.StartTime,
            UserId = model.UserId,
            WorkDate = model.WorkDate,
        };


        return workUnit;
    }
}