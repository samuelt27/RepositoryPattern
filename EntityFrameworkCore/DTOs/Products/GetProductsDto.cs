using RepositoryPattern.EntityFrameworkCore.Entities;

namespace RepositoryPattern.EntityFrameworkCore.DTOs.Products;

public record GetProductsDto(
    int Id,
    string Name
)
{
    public static explicit operator GetProductsDto(Product product)
    {
        return new GetProductsDto(
            Id: product.Id,
            Name: product.Name
        );
    }
}