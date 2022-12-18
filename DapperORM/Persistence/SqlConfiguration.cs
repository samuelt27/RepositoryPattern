namespace RepositoryPattern.DapperORM.Persistence;

public class SqlConfiguration
{
    public SqlConfiguration(string connectionString) => ConnectionString = connectionString;


    public string ConnectionString { get; }
}