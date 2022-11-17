namespace AUTimeManagement.Api.DataAccess.Layer.Model.Internal;

internal class DataModelBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
