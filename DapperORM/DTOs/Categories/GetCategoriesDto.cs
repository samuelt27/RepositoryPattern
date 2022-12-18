using RepositoryPattern.DapperORM.Entities;

namespace RepositoryPattern.DapperORM.DTOs.Categories;

public record GetCategoriesDto(
    int Id,
    string Name)
{
    public static explicit operator GetCategoriesDto(Category category)
    {
        return new GetCategoriesDto(
            Id: category.Id,
            Name: category.Name
        );
    }
}