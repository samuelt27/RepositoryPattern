using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.EntityFrameworkCore.Entities;

namespace RepositoryPattern.EntityFrameworkCore.Persistence.Repositories;

public interface IProductRepository : IGenericRepository<Product> { }


public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext) { }


    public override async Task<Product?> GetAsync(Expression<Func<Product, bool>> predicate)
    {
        return await DbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(predicate);
    }
}