using MediatR;

namespace Domain.Commands.Articles.ListArticles
{
    public class ListArticleRequest : IRequest<Response>
    {
        public ListArticleRequest(int? categoryId)
        {
            CategoryId = categoryId;
        }

        public int? CategoryId { get; set; }
    }
}
