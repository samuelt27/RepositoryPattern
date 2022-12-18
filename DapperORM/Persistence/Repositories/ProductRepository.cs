using System.Data;
using System.Data.SqlClient;
using Dapper;
using RepositoryPattern.DapperORM.Entities;

namespace RepositoryPattern.DapperORM.Persistence.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();

    Task<Product?> GetProductAsync(object id);

    Task CreateProductAsync(Product product);

    Task UpdateProductAsync(Product product);

    Task DeleteProductAsync(Product product);
}


public class ProductRepository : IProductRepository
{
    private readonly IDbConnection _dbConnection;
    
    
    public ProductRepository(SqlConfiguration sqlConfiguration)
    {
        _dbConnection = new SqlConnection(sqlConfiguration.ConnectionString);
    }
    
    
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        const string query = @"SELECT *
                               FROM Products;";

        return await _dbConnection.QueryAsync<Product>(sql: query, param: null);
    }

    public async Task<Product?> GetProductAsync(object id)
    {
        const string query = @"SELECT *
                               FROM Products p
                               INNER JOIN Categories c ON p.CategoryId = c.Id
                               WHERE p.Id = @Id;";

        var @params = new DynamicParameters();
        @params.AddDynamicParams(new {Id = id});

        return (await _dbConnection.QueryAsync<Product, Category, Product>(
            sql: query,
            map: (product, category) =>
            {
                product.Category = category;

                return product;
            },
            param: @params
        )).FirstOrDefault();
    }

    public async Task CreateProductAsync(Product product)
    {
        const string query = @"INSERT INTO Products (CategoryId, Name, Price, Description)
                               VALUES (@CategoryId, @Name, @Price, @Description);";

        var @params = new DynamicParameters();
        @params.Add("CategoryId", product.CategoryId, DbType.Int32);
        @params.Add("Name", product.Name, DbType.String);
        @params.Add("Price", product.Price, DbType.Decimal);
        @params.Add("Description", product.Description, DbType.String);

        await _dbConnection.ExecuteAsync(sql: query, param: @params);
    }

    public async Task UpdateProductAsync(Product product)
    {
        const string query = @"UPDATE Products
                               SET CategoryId = @CategoryId, Name = @Name, Price = @Price, Description = @Description
                               WHERE Id = @Id;";
        
        var @params = new DynamicParameters();
        @params.Add("CategoryId", product.CategoryId, DbType.Int32);
        @params.Add("Name", product.Name, DbType.String);
        @params.Add("Price", product.Price, DbType.Decimal);
        @params.Add("Description", product.Description, DbType.String);
        @params.AddDynamicParams(new {Id = product.Id});

        await _dbConnection.ExecuteAsync(sql: query, param: @params);
    }

    public async Task DeleteProductAsync(Product product)
    {
        const string query = @"DELETE FROM Products
                               WHERE Id = @Id;";
        
        var @params = new DynamicParameters();
        @params.AddDynamicParams(new {Id = product.Id});

        await _dbConnection.ExecuteAsync(sql: query, param: @params);
    }
}