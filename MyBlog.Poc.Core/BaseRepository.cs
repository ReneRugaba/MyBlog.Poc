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

    public async Task<IEnumerable<TEntity>> AllAsync<TEntity>(ISpecification<TEntity> specification) where TEntity : BaseEntity
    {
        _ = specification ?? throw new ArgumentException($"{nameof(specification)} can't be null in this scope.");

        var result = specification.Includes.Aggregate(
            _context.Set<TEntity>().AsQueryable(),
            (current, IncludeExpression) => current.Include(IncludeExpression));

        var secondResult = specification.IncludeStrings.Aggregate(
        result.AsQueryable(),
        (current, IncludesExpression) => current.Include(IncludesExpression));

        if (specification.IsPagingEnabled)
        {
            var pageIndex = specification.Skip ?? 0;
            var pageSize = specification.Take ?? 10;

            secondResult = secondResult.OrderBy(a => a).Skip(pageSize * pageIndex).Take(pageSize);
        }

        if (specification.IsSplitQuery)
        {
            secondResult = secondResult.AsSplitQuery();
        }

        return await secondResult.ToListAsync();
    }

    public async Task<TEntity> FindByIdAsync<TEntity>(ISpecification<TEntity> specification) where TEntity : BaseEntity
    {
        var result = specification.Includes.Aggregate(
            _context.Set<TEntity>().AsQueryable(),
            (current, IncludeExpression) => current.Include(IncludeExpression));

        result = specification.IncludeStrings.Aggregate(
            result.AsQueryable(),
            (current, IncludesExpression) => current.Include(IncludesExpression));

        return await result.FirstAsync();
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
