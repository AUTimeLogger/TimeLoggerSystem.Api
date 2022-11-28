namespace AUTimeManagement.Api.DataAccess.Layer;

public class DataAccessOptions
{
    public enum DatabaseType
    {
        SQL,
        Postgres,
        InMemory,
    }

    private DatabaseType _database = DatabaseType.SQL;

    public string? ConnectionString { get; set; }
    public DatabaseType Database
    {
        get => _database;
        set => _database = value;
    }


}
