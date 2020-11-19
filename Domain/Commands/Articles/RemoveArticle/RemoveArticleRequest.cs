using MediatR;

namespace Domain.Commands.Articles.RemoveArticle
{
    public class RemoveArticleRequest : IRequest<Response>
    {
        public RemoveArticleRequest(long id)
        {
            Id = id;
        }
        public long Id { get; set; }        
    }
}
