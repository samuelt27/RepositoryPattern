using System.Data;
using System.Data.SqlClient;
using Dapper;
using RepositoryPattern.DapperORM.Entities;

namespace RepositoryPattern.DapperORM.Persistence.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();

    Task<Category?> GetCategoryAsync(object id);

    Task CreateCategoryAsync(Category category);

    Task UpdateCategoryAsync(Category category);

    Task DeleteCategoryAsync(Category category);
}


public class CategoryRepository : ICategoryRepository
{
    private readonly IDbConnection _dbConnection;
    
    
    public CategoryRepository(SqlConfiguration sqlConfiguration)
    {
        _dbConnection = new SqlConnection(sqlConfiguration.ConnectionString);
    }
    
    
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        const string query = @"SELECT *
                               FROM Categories;";

        return await _dbConnection.QueryAsync<Category>(sql: query, param: null);
    }

    public async Task<Category?> GetCategoryAsync(object id)
    {
        const string query = @"SELECT *
                               FROM Categories
                               WHERE Id = @Id;";

        var @params = new DynamicParameters();
        @params.AddDynamicParams(new {Id = id});

        return await _dbConnection.QueryFirstOrDefaultAsync<Category>(sql: query, param: @params);
    }

    public async Task CreateCategoryAsync(Category category)
    {
        const string query = @"INSERT INTO Categories (Name)
                               VALUES (@Name);";

        var @params = new DynamicParameters();
        @params.Add("Name", category.Name, DbType.String);

        await _dbConnection.ExecuteAsync(sql: query, param: @params);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        const string query = @"UPDATE Categories
                               SET Name = @Name
                               WHERE Id = @Id;";

        var @params = new DynamicParameters();
        @params.Add("Name", category.Name, DbType.String);
        @params.AddDynamicParams(new {Id = category.Id});

        await _dbConnection.ExecuteAsync(sql: query, param: @params);
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        const string query = @"DELETE FROM Categories
                               WHERE Id = @Id;";

        var @params = new DynamicParameters();
        @params.AddDynamicParams(new {Id = category.Id});

        await _dbConnection.ExecuteAsync(sql: query, param: @params);
    }
}