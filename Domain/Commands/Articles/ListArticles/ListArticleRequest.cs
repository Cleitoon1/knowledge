using MediatR;

namespace Domain.Commands.Articles.ListArticles
{
    public class ListArticleRequest : IRequest<Response>
    {
        public int? CategoryId { get; set; }
        public int? Page { get; set; }
        public int? Quantity { get; set; }
    }
}
