using RepositoryPattern.DapperORM.DTOs.Categories;
using RepositoryPattern.DapperORM.Entities;
using RepositoryPattern.DapperORM.Persistence.Repositories;

namespace RepositoryPattern.DapperORM.Services;

public interface ICategoriesService
{
    Task<IEnumerable<GetCategoriesDto>> GetCategoriesAsync();

    Task<int> CreateCategoryAsync(CreateCategoryDto categoryDto);

    Task UpdateCategoryAsync(int id, CreateCategoryDto categoryDto);

    Task DeleteCategoryAsync(int id);
}


public class CategoriesService : ICategoriesService
{
    private readonly ICategoryRepository _repository;

    
    public CategoriesService(ICategoryRepository repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<GetCategoriesDto>> GetCategoriesAsync() =>
        (await _repository.GetCategoriesAsync()).Select(c => (GetCategoriesDto) c);

    public async Task<int> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = new Category {Name = categoryDto.Name};

        await _repository.CreateCategoryAsync(category);

        return category.Id;
    }

    public async Task UpdateCategoryAsync(int id, CreateCategoryDto categoryDto)
    {
        var category = await _repository.GetCategoryAsync(id);

        if (category is null)
            throw new KeyNotFoundException();

        category.Name = categoryDto.Name;

        await _repository.UpdateCategoryAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _repository.GetCategoryAsync(id);

        if (category is null)
            throw new KeyNotFoundException();

        await _repository.DeleteCategoryAsync(category);
    }
}