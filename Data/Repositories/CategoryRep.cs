using Data.Repositories.Base;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Data.Repositories
{
    public class CategoryRep : BaseRep<Category, long>, ICategoryRep
    {
        public CategoryRep(KnowledgeContext context) : base(context)
        {
        }

        public Category GetTree(long id)
        {
            Category category = GetById(id, false);
            if (category.ParentCategoryId.HasValue)
            {
                category.SetParent(GetTree(category.ParentCategoryId.Value));
            }
            return category;
        }
    }
}
