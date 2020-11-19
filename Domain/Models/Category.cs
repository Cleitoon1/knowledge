using Domain.Models.Base;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public Category(string name, long? parentCategoryId = null) : base()
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
        }

        public string Name { get; private set; }
        public long? ParentCategoryId { get; private set; }
        public Category ParentCategory { get; private set; }
        public ICollection<Article> Articles { get; private set; }
        public ICollection<Category> Parents { get; private set; }
        public void SetParent(Category category)
        {
            ParentCategoryId = category.Id;
            ParentCategory = category;
        }

        public string GetPath()
        {
            string path = Name;
            if (ParentCategory != null)
                path += $"{Name} >> { ParentCategory.GetPath() }";
            return path;
        }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .HasMaxLengthIfNotNullOrEmpty(Name, 50, "Name", "Name must not null, empty or exceed 50 chars"));
        }

        public void UpdateArticle(string name, long? parentCategoryId)
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
        }
    }
}
