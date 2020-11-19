using Domain.Interfaces.Repositories.Base;
using Domain.Models;
using System.Collections.Generic;

namespace Domain.Interfaces.Repositories
{
    public interface IArticleRep : IBaseRep<Article, long>
    {
        public IEnumerable<Article> GetByCategory(long categoryId);
    }
}
