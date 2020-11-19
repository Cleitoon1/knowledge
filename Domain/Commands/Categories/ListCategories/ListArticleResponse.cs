using Domain.Models;
using System;

namespace Domain.Commands.Categories.ListCategories
{
    public class ListArticleResponse
    {

        public ListArticleResponse(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentCategoryId = category.ParentCategoryId;
            Path = category.GetPath();
            CreatedDate = category.CreatedDate;
            ModifiedDate = category.ModifiedDate;
        }
        public ListArticleResponse(string name, long? parentCategoryId, string path, DateTime createdDate, DateTime modifiedDate)
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
            Path = path;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
