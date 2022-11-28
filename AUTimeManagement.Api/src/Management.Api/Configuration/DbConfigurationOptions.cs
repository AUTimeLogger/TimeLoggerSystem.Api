namespace AUTimeManagement.Api.Management.Api.Configuration
{
    public class DbConfigurationOptions
    {
        public const string SectionName = "SecurityDbConfig";
        public enum DatabaseType
        {
            InMemory,
            Sql,
            Postgres
        };

        public DatabaseType Database { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
    }
}
