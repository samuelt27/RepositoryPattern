using RepositoryPattern.EntityFrameworkCore.Entities;

namespace RepositoryPattern.EntityFrameworkCore.DTOs.Categories;

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