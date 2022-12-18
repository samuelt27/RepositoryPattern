using RepositoryPattern.EntityFrameworkCore.DTOs.Categories;
using RepositoryPattern.EntityFrameworkCore.Entities;
using RepositoryPattern.EntityFrameworkCore.Persistence.Repositories;

namespace RepositoryPattern.EntityFrameworkCore.Services;

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
        (await _repository.GetAllAsync()).Select(c => (GetCategoriesDto) c);

    public async Task<int> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = new Category {Name = categoryDto.Name};

        await _repository.CreateAsync(category);
        await _repository.SaveChangesAsync();

        return category.Id;
    }

    public async Task UpdateCategoryAsync(int id, CreateCategoryDto categoryDto)
    {
        var category = await _repository.FindAsync(predicate: c => c.Id == id);

        if (category is null)
            throw new KeyNotFoundException();

        category.Name = categoryDto.Name;
        
        await _repository.UpdateAsync(category);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _repository.FindAsync(predicate: c => c.Id == id);

        if (category is null)
            throw new KeyNotFoundException();
        
        await _repository.DeleteAsync(category);
        await _repository.SaveChangesAsync();
    }
}