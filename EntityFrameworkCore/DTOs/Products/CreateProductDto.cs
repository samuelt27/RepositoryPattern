namespace RepositoryPattern.EntityFrameworkCore.DTOs.Products;

public record CreateProductDto(
    int CategoryId,
    string Name,
    decimal Price,
    string? Description
);