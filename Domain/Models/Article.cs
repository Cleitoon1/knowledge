using Domain.Models.Base;
using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Article : BaseEntity
    {
        public Article(string title, string description, string imageUrl, byte[] content, 
            long categoryId, long userId) : base()
        {
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Content = content;
            CategoryId = categoryId;
            UserId = userId;
            Validate();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public byte[] Content { get; private set; }
        public long CategoryId { get; private set; }
        public Category Category { get; private set; }
        public long UserId { get; set; }
        public User User { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .HasMaxLengthIfNotNullOrEmpty(Title, 100, "Title", "Title cannot be null, empty or exceed 100 chars")
                .HasMaxLen(Description, 1000, "Description", "Description must not exceed 1000 chars")
                .IsNotNull(Content, "Content", "Content cannot be null")
                .IsGreaterThan(CategoryId, 0, "CategoryId", "CategoryId must be valid")
                .IsGreaterThan(UserId, 0, "UserId", "UserId must be valid")
            );
        }

        public void UpdateArticle(string title, string description, string imageUrl, byte[] content, int categoryId)
        {
            Title = title;
            Description = description;
            Content = content;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
            Validate();
        }
    }
}
