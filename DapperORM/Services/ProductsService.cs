using RepositoryPattern.DapperORM.DTOs.Products;
using RepositoryPattern.DapperORM.Entities;
using RepositoryPattern.DapperORM.Persistence.Repositories;

namespace RepositoryPattern.DapperORM.Services;

public interface IProductsService
{
    Task<IEnumerable<GetProductsDto>> GetProductsAsync();

    Task<GetProductDetailsDto> GetProductAsync(int id);

    Task<int> CreateProductAsync(CreateProductDto productDto);

    Task UpdateProductAsync(int id, CreateProductDto productDto);

    Task DeleteProductAsync(int id);
}


public class ProductsService : IProductsService
{
    private readonly IProductRepository _repository;

    
    public ProductsService(IProductRepository repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<GetProductsDto>> GetProductsAsync() =>
        (await _repository.GetProductsAsync()).Select(p => (GetProductsDto) p);

    public async Task<GetProductDetailsDto> GetProductAsync(int id)
    {
        var product = await _repository.GetProductAsync(id);

        return product is null
            ? throw new KeyNotFoundException()
            : (GetProductDetailsDto) product;
    }

    public async Task<int> CreateProductAsync(CreateProductDto productDto)
    {
        var product = new Product
        {
            CategoryId = productDto.CategoryId,
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description
        };

        await _repository.CreateProductAsync(product);

        return product.Id;
    }

    public async Task UpdateProductAsync(int id, CreateProductDto productDto)
    {
        var product = await _repository.GetProductAsync(id);

        if (product is null)
            throw new KeyNotFoundException();

        product.CategoryId = productDto.CategoryId;
        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.Description = productDto.Description;

        await _repository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _repository.GetProductAsync(id);

        if (product is null)
            throw new KeyNotFoundException();

        await _repository.DeleteProductAsync(product);
    }
}