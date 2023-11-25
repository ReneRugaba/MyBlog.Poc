using MyBlog.Poc.Core.Entities;
using System.Linq.Expressions;

namespace MyBlog.Poc.Core;

public abstract class BaseQuerySpecification<T> : ISpecification<T> where T : BaseEntity
{

    public BaseQuerySpecification(Expression<Func<T, bool>>? criteria = null) => Criteria = criteria;

    public Expression<Func<T, bool>>? Criteria { get; set; }

    public List<Expression<Func<T, object>>> Includes { get; set; } = new();

    public List<string> IncludeStrings { get; set; } = new();

    public Expression<Func<T, object>>? OrderBy { get; set; }

    public Expression<Func<T, object>>? OrderByDescending { get; set; }

    public Expression<Func<T, object>>? GroupBy { get; set; }
    public Expression<Func<T, object>>? ThenInclude { get; set; }
    public Expression<Func<T, object>>? Select { get; set; }

    public int? Take { get; set; }

    public int? Skip { get; set; }

    public bool IsSplitQuery { get; set; }

    public bool IsPagingEnabled { get; set; }


    protected virtual void AddInclude(Expression<Func<T, object>>? includeExpression)
    {
        if (includeExpression is not null)
            Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
}
