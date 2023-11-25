using Microsoft.EntityFrameworkCore;
using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc.Core;

public class BaseRepository<CTX> : IBaseRepository<CTX> where CTX : DbContext
{
    private readonly DbContext _context;

    public BaseRepository(CTX context)
    {
        _context = context;
    }

    public Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity => Task.FromResult(_context.AddAsync(entity));

    public Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity => _context.AddRangeAsync(entities);

    public async Task<IEnumerable<TEntity>> AllAsync<TEntity>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) where TEntity : BaseEntity
    {
        return await SpecificationEvaluator.Evaluate(
            _context.Set<TEntity>().AsQueryable(), specification
            ).ToListAsync(cancellationToken);
    }

    public async Task<TEntity> FindByAsync<TEntity>(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) where TEntity : BaseEntity
    {
        return await SpecificationEvaluator.Evaluate(
            _context.Set<TEntity>().AsQueryable(), specification
            ).FirstAsync(cancellationToken);
    }

    public Task RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        throw new NotImplementedException();
    }

    public Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
    {
        throw new NotImplementedException();
    }

    public Task SaveChangeAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        throw new NotImplementedException();
    }
}
