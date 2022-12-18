using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RepositoryPattern.EntityFrameworkCore.Persistence.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> GetQueryAsync();
    Task<IQueryable<TEntity>> FindQueryAsync();

    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAllAsync();

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task CreateAsync(TEntity entity);
    Task CreateRangeAsync(IEnumerable<TEntity> entities);

    Task PatchAsync(TEntity entity);
    void PatchRange(IEnumerable<TEntity> entities);

    Task UpdateAsync(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);

    Task DeleteAsync(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);

    Task SaveChangesAsync();
}


public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext DbContext;

    
    public GenericRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }


    public virtual Task<IQueryable<TEntity>> GetQueryAsync() => Task.FromResult(DbContext.Set<TEntity>().AsNoTracking());

    public virtual Task<IQueryable<TEntity>> FindQueryAsync() => Task.FromResult(DbContext.Set<TEntity>().AsQueryable());
    

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await DbContext.Set<TEntity>().AsNoTracking().ToListAsync();

    public virtual async Task<IEnumerable<TEntity>> FindAllAsync() => await DbContext.Set<TEntity>().ToListAsync();
    

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate) => await DbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);

    public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate) => await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
    

    public async Task CreateAsync(TEntity entity) => await DbContext.Set<TEntity>().AddAsync(entity);

    public async Task CreateRangeAsync(IEnumerable<TEntity> entities) => await DbContext.Set<TEntity>().AddRangeAsync(entities);
    

    public Task PatchAsync(TEntity entity) => Task.FromResult(DbContext.Set<TEntity>().Attach(entity));

    public void PatchRange(IEnumerable<TEntity> entities) => DbContext.Set<TEntity>().AttachRange(entities);
    

    public Task UpdateAsync(TEntity entity) => Task.FromResult(DbContext.Set<TEntity>().Update(entity));

    public void UpdateRange(IEnumerable<TEntity> entities) => DbContext.Set<TEntity>().UpdateRange(entities);
    

    public Task DeleteAsync(TEntity entity) => Task.FromResult(DbContext.Set<TEntity>().Remove(entity));

    public void DeleteRange(IEnumerable<TEntity> entities) => DbContext.Set<TEntity>().RemoveRange(entities);
    

    public async Task SaveChangesAsync() => await DbContext.SaveChangesAsync();
}