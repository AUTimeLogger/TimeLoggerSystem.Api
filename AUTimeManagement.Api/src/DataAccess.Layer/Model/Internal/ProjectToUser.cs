namespace AUTimeManagement.Api.DataAccess.Layer.Model.Internal
{
    internal class ProjectToUser
    {
        public Guid UserId { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
