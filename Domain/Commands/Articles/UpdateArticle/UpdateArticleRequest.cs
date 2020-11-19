using MediatR;

namespace Domain.Commands.Articles.UpdateArticle
{
    public class UpdateArticleRequest : IRequest<Response>
    {
        public UpdateArticleRequest(long id, string title, string description, string imageUrl, byte[] content, int categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Content = content;
            CategoryId = categoryId;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte[] Content { get; set; }
        public int CategoryId { get; set; }
    }
}
