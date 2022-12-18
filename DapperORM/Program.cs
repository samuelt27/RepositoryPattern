using RepositoryPattern.DapperORM.Modules;
using RepositoryPattern.DapperORM.Persistence;
using RepositoryPattern.DapperORM.Persistence.Repositories;
using RepositoryPattern.DapperORM.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

builder.Services.AddSingleton(new SqlConfiguration(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

var app = builder.Build();

#region Modules

var modules = typeof(Program).Assembly
    .GetTypes()
    .Where(t => t.IsClass && t.IsAssignableTo(typeof(IModule)))
    .Select(Activator.CreateInstance)
    .Cast<IModule>();

foreach (var module in modules)
    module.MapEndpoints(app);

#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi3();
    app.UseOpenApi();
}

app.Run();