using Microsoft.EntityFrameworkCore;
using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc.Core;

public interface IBaseRepository<CTX> where CTX : DbContext
{
    Task<TEntity> FindByIdAsync<TEntity>(ISpecification<TEntity> specification) where TEntity : BaseEntity;
    Task<IEnumerable<TEntity>> AllAsync<TEntity>(ISpecification<TEntity> specification) where TEntity : BaseEntity;
    Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
    Task RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    Task RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
    Task UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    Task SaveChangeAsync();
}
