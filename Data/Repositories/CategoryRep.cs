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

        public IEnumerable<Category> GetTree(long? parentId = null)
        {
            IQueryable<Category> query = _context.Set<Category>().AsNoTracking();
            query = parentId.HasValue ? 
                query.Where(x => x.ParentId == parentId.Value) :
                query.Where(x => x.ParentId == null);
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
            Category category = GetById(id, false, x => x.Childrens);
            if (category.ParentId.HasValue)
                category.SetParent(GetPath(category.ParentId.Value));            
            return category;
        }

        public IEnumerable<Category> GetChildrens(long parentId)
            => _context.Set<Category>().Where(x => x.ParentId == parentId).ToList();
        
    }
}
