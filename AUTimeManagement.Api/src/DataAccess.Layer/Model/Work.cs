using AUTimeManagement.Api.DataAccess.Layer.Model.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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