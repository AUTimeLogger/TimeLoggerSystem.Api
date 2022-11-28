using AUTimeManagement.Api.DataAccess.Layer.Model.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTimeManagement.Api.DataAccess.Layer.Model;

public sealed class CreateWorkModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Duration { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime WorkDate { get; set; }
}


internal static class CreateWorkModelExtensions
{
    public static WorkUnit ToWorkUnit(this CreateWorkModel model)
    {
        var workUnit = new WorkUnit
        {
            Comment = model.Comment,
            Duration = model.Duration,
            EndDate = model.EndDate,
            StartDate = model.StartDate,
            WorkDate = model.WorkDate,
            
        };


        return workUnit;
    }
}