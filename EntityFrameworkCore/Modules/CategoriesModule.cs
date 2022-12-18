using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.EntityFrameworkCore.DTOs.Categories;
using RepositoryPattern.EntityFrameworkCore.Services;

namespace RepositoryPattern.EntityFrameworkCore.Modules;

public class CategoriesModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder endpoint)
    {
        const string route = "api/categories";
        const string tag = "Categories";
        
        
        // GET
        endpoint.MapGet($"{route}", async (ICategoriesService service) =>
            await service.GetCategoriesAsync()
        ).WithTags(tag);
        
        // POST
        endpoint.MapPost($"{route}",
            async (ICategoriesService service, [FromBody] CreateCategoryDto categoryDto) =>
            {
                await service.CreateCategoryAsync(categoryDto);

                Results.NoContent();
            }
        ).WithTags(tag);
        
        // PUT
        endpoint.MapPut($"{route}/{{id:int}}",
            async (ICategoriesService service, int id, [FromBody] CreateCategoryDto categoryDto) =>
            {
                await service.UpdateCategoryAsync(id, categoryDto);

                Results.NoContent();
            }
        ).WithTags(tag);
        
        // DELETE
        endpoint.MapDelete($"{route}/{{id:int}}",
            async (ICategoriesService service, int id) =>
            {
                await service.DeleteCategoryAsync(id);

                Results.NoContent();
            }
        ).WithTags(tag);
    }
}