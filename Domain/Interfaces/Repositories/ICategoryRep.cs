using System.Collections.Generic;
using Domain.Interfaces.Repositories.Base;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ICategoryRep : IBaseRep<Category, long>
    {
        Category GetPath(long id);
        IEnumerable<Category> GetChildrens(long parentCategoryId);
        IEnumerable<Category> GetTree(long? parentCategoryId = null);
    }
}
