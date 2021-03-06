using System.Linq;
using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Commands.Categories.TreeCategories
{
    public class TreeCategoriesResponse
    {
        public TreeCategoriesResponse(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.ParentId;
            CreatedDate = category.CreatedDate;
            ModifiedDate = category.ModifiedDate;
            Children = (category.Childrens ?? new List<Category>())
                .Select(x => new TreeCategoriesResponse(x)).ToList();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public IEnumerable<TreeCategoriesResponse> Children { get; set; } = new List<TreeCategoriesResponse>();
    }
}