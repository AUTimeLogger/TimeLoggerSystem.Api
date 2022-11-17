namespace AUTimeManagement.Api.DataAccess.Layer.Model.Internal;

internal class WorkUnit : DataModelBase
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Duration { get; set; }
    public string Comment { get; set; }
    public DateTime WorkDate { get; set; }

    public Guid? ProjectId { get; set; }
    public Project Project { get; set; }
}
