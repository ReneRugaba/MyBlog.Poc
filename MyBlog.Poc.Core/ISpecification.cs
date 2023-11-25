﻿using System.Linq.Expressions;

namespace MyBlog.Poc.Core;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>>? ThenInclude { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    Expression<Func<T, object>>? GroupBy { get; }


    int? Take { get; }
    int? Skip { get; }
    bool IsPagingEnabled { get; }
    bool IsSplitQuery { get; }
}
