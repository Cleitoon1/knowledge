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
            ParentId = category.ParentId;
            Path = category.GetPath();
            CreatedDate = category.CreatedDate;
            ModifiedDate = category.ModifiedDate;
        }
        public ListArticleResponse(string name, long? parentId, string path, DateTime createdDate, DateTime modifiedDate)
        {
            Name = name;
            ParentId = parentId;
            Path = path;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
