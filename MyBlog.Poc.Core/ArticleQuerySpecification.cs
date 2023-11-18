﻿using MyBlog.Poc.Core.Entities;
using System.Linq.Expressions;

namespace MyBlog.Poc.Core;

public class ArticleQuerySpecification : BaseQuerySpecification<Article>
{
    public ArticleQuerySpecification(Expression<Func<Article, bool>>? criteria = null) : base(criteria)
    {
    }

    public ArticleQuerySpecification(int id) : base(a => a.ArticleId == id)
    {
        AddInclude(a => a.Author);
        AddInclude(a => a.Commentaries);
    }
}
