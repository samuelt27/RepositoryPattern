using RepositoryPattern.EntityFrameworkCore.Entities;

namespace RepositoryPattern.EntityFrameworkCore.Persistence.Repositories;

public interface ICategoryRepository : IGenericRepository<Category> { }


public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext) { }
}