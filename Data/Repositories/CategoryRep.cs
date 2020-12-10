using System.Linq;
using System.Collections.Generic;
using Data.Repositories.Base;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoryRep : BaseRep<Category, long>, ICategoryRep
    {
        public CategoryRep(KnowledgeContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetTree(long? parentCategoryId = null)
        {
            IQueryable<Category> query = _context.Set<Category>().AsNoTracking();
            query = parentCategoryId.HasValue ? 
                query.Where(x => x.ParentCategoryId == parentCategoryId.Value) :
                query.Where(x => x.ParentCategoryId == null);
            return query.ToList().Select(x => ToTree(x)).ToList();            
        }

        private Category ToTree(Category category)
        {
            foreach (Category item in GetChildrens(category.Id).ToList())
                category.AddChildren(ToTree(item));
            return category;
        }
        

        public Category GetPath(long id)
        {
            Category category = GetById(id, false, x => x.Parents);
            if (category.ParentCategoryId.HasValue)
                category.SetParent(GetPath(category.ParentCategoryId.Value));            
            return category;
        }

        public IEnumerable<Category> GetChildrens(long parentCategoryId)
            => _context.Set<Category>().Where(x => x.ParentCategoryId == parentCategoryId).ToList();
        
    }
}
