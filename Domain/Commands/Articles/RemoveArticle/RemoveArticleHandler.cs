using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Articles.RemoveArticle
{
    class RemoveArtcileHandler : Notifiable, IRequestHandler<RemoveArticleRequest, Response>
    {
        private readonly IArticleRep _articleRep;

        public RemoveArtcileHandler(IArticleRep articleRep)
        {
            _articleRep = articleRep;
        }

        public async Task<Response> Handle(RemoveArticleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }

            Article article = _articleRep.GetById(request.Id);
            if (article == null)
            {
                AddNotification("Article", "There is no article with this Id");
                return new Response(this);
            }
            _articleRep.Delete(request.Id);
            return await Task.FromResult(new Response(this, new { request.Id }));
        }
    }
}
