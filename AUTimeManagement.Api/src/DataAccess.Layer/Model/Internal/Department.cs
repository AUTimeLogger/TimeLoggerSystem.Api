namespace AUTimeManagement.Api.DataAccess.Layer.Model.Internal;

internal sealed class Department : DataModelBase
{
    public string DepartmentName { get; set; } = "";
    public Guid DepartmentDirector { get; set; }
}
