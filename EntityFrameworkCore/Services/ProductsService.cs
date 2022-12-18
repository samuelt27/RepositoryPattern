using RepositoryPattern.EntityFrameworkCore.DTOs.Products;
using RepositoryPattern.EntityFrameworkCore.Entities;
using RepositoryPattern.EntityFrameworkCore.Persistence.Repositories;

namespace RepositoryPattern.EntityFrameworkCore.Services;

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
        (await _repository.GetAllAsync()).Select(p => (GetProductsDto) p);

    public async Task<GetProductDetailsDto> GetProductAsync(int id)
    {
        var product = await _repository.GetAsync(predicate: p => p.Id == id);

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

        await _repository.CreateAsync(product);
        await _repository.SaveChangesAsync();

        return product.Id;
    }

    public async Task UpdateProductAsync(int id, CreateProductDto productDto)
    {
        var product = await _repository.FindAsync(predicate: p => p.Id == id);

        if (product is null)
            throw new KeyNotFoundException();

        product.CategoryId = productDto.CategoryId;
        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.Description = productDto.Description;

        await _repository.UpdateAsync(product);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _repository.FindAsync(predicate: p => p.Id == id);

        if (product is null)
            throw new KeyNotFoundException();

        await _repository.DeleteAsync(product);
        await _repository.SaveChangesAsync();
    }
}