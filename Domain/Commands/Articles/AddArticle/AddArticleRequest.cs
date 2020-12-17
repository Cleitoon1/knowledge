using MediatR;

namespace Domain.Commands.Articles.AddArticle
{
    public class AddArticleRequest : IRequest<Response>
    {
        public AddArticleRequest(string title, string description, string imageUrl, 
            byte[] content, int categoryId, int userId)
        {
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Content = content;
            CategoryId = categoryId;
            UserId = userId;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte[] Content { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
