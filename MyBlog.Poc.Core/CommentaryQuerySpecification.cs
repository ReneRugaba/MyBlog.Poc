using MyBlog.Poc.Core.Entities;

namespace MyBlog.Poc.Core;

public class CommentaryQuerySpecification : BaseQuerySpecification<Commentary>
{
    public CommentaryQuerySpecification(int arcticleId) : base(c => c.ArticleId == arcticleId)
    {
        AddInclude(c => c.Author);
    }
}
