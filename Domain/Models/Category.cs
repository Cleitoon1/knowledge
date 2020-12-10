using Domain.Models.Base;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public Category(string name, long? parentId = null) : base()
        {
            Name = name;
            ParentId = parentId;
        }

        public string Name { get; private set; }
        public long? ParentId { get; private set; }
        public Category Parent { get; private set; }
        public ICollection<Article> Articles { get; private set; }
        public ICollection<Category> Childrens { get; private set; }
        public void SetParent(Category category)
        {
            ParentId = category.Id;
            Parent = category;
        }

        public void SetParents(IList<Category> childrens)
        {
            Childrens = childrens;
        }

        public void AddChildren(Category category)
        {
            if(Childrens == null)
                Childrens = new List<Category>();
            Childrens.Add(category);
        }

        public string GetPath()
        {
            string path = Name;
            if (Parent != null)
                path = $"{Parent.GetPath()} >> {Name}";
            return path;
        }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .HasMaxLengthIfNotNullOrEmpty(Name, 50, "Name", "Name must not null, empty or exceed 50 chars"));
        }

        public void UpdateArticle(string name, long? parentId)
        {
            Name = name;
            ParentId = parentId;
        }
    }
}
