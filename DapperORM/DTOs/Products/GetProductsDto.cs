using RepositoryPattern.DapperORM.Entities;

namespace RepositoryPattern.DapperORM.DTOs.Products;

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