using Microsoft.EntityFrameworkCore;
using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc.Core;

public static class SpecificationEvaluator
{
    private static readonly int _minSkipValue = 0;
    private static readonly int _maxTakeValue = 10;
    public static IQueryable<TEntity> Evaluate<TEntity>(
        IQueryable<TEntity> inputQueryable,
        ISpecification<TEntity>? specification) where TEntity : BaseEntity
    {
        _ = specification ?? throw new ArgumentException($"{nameof(specification)} can't be null in this scope.");

        var result = specification.Includes.Aggregate(
           inputQueryable,
           (current, IncludeExpression) => current.Include(IncludeExpression));

        var secondResult = specification.IncludeStrings.Aggregate(
            result,
            (current, IncludesExpression) => current.Include(IncludesExpression));

        if (specification.IsSplitQuery)
        {
            secondResult = secondResult.AsSplitQuery();
        }
        if (specification.Criteria != null)
        {
            secondResult = secondResult.Where(specification.Criteria);
        }
        if (specification.OrderBy is not null)
        {
            secondResult = secondResult.OrderBy(specification.OrderBy);
        }
        if (specification.OrderByDescending is not null)
        {
            secondResult = secondResult.OrderByDescending(specification.OrderByDescending);
        }
        if (specification.IsPagingEnabled)
        {
            var pageIndex = specification.Skip ?? _minSkipValue;
            var pageSize = specification.Take ?? _maxTakeValue;

            secondResult = secondResult.Skip(pageSize * pageIndex).Take(pageSize);
        }

        return secondResult;
    }
}
