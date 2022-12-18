using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.DapperORM.DTOs.Products;
using RepositoryPattern.DapperORM.Services;

namespace RepositoryPattern.DapperORM.Modules;

public class ProductModule : IModule
{
    public void MapEndpoints(IEndpointRouteBuilder endpoint)
    {
        const string route = "api/products";
        const string tag = "Products";

        
        // GET
        endpoint.MapGet($"{route}", async (IProductsService service) => 
            await service.GetProductsAsync()
        ).WithTags(tag);
        
        endpoint.MapGet($"{route}/{{id:int}}", async (IProductsService service, int id) => 
            await service.GetProductAsync(id)
        ).WithTags(tag);
        
        // POST
        endpoint.MapPost($"{route}",
            async (IProductsService service, [FromBody] CreateProductDto productDto) => 
                Results.Ok(await service.CreateProductAsync(productDto))
        ).WithTags(tag);
        
        // PUT
        endpoint.MapPut($"{route}/{{id:int}}",
            async (IProductsService service, int id, [FromBody] CreateProductDto productDto) => 
            {
                await service.UpdateProductAsync(id, productDto);

                return Results.NoContent();
            }
        ).WithTags(tag);
        
        // DELETE
        endpoint.MapDelete($"{route}/{{id:int}}",
            async (IProductsService service, int id) =>
            {
                await service.DeleteProductAsync(id);
                
                return Results.NoContent();
            }
        ).WithTags(tag);
    }
}