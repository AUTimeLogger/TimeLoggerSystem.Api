namespace AUTimeManagement.Api.DataAccess.Layer.Model.Internal;

internal sealed class Project : DataModelBase
{
    public string ProjectName { get; set; } = "";
    public List<ProjectToUser> Users { get; set; } = new();
    public List<WorkUnit> WorkUnits { get; set; } = new();
}
