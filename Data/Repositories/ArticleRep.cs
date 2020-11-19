using Data.Repositories.Base;
using Domain.Interfaces.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class ArticleRep : BaseRep<Article, long>, IArticleRep
    {
        public ArticleRep(KnowledgeContext context) : base(context)
        {
        }

        public IEnumerable<Article> GetByCategory(long categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
