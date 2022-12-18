using RepositoryPattern.DapperORM.Entities;

namespace RepositoryPattern.DapperORM.DTOs.Products;

public record GetProductDetailsDto(
    int Id,
    object Category,
    string Name,
    decimal Price,
    string? Description
)
{
    public static explicit operator GetProductDetailsDto(Product product)
    {
        return new GetProductDetailsDto(
            Id: product.Id,
            Category: new {Id = product.CategoryId, Name = product.Category.Name},
            Name: product.Name,
            Price: product.Price,
            Description: product.Description
        );
    }
}