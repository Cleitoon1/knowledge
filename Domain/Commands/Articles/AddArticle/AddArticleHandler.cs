using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Articles.AddArticle
{
    public class AddArticleHandler : Notifiable, IRequestHandler<AddArticleRequest, Response>
    {
        private readonly IArticleRep _articleRep;

        public AddArticleHandler(IArticleRep articleRep)
        {
            _articleRep = articleRep;
        }

        public async Task<Response> Handle(AddArticleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }
            Article article = new Article(request.Title, request.Description, request.ImageUrl,
                request.Content, request.CategoryId, request.UserId);
            AddNotifications(article);

            if (Invalid)
                return new Response(this);

            article = _articleRep.Create(article);
            var result = new { article.Id, article.Title, article.Description, article.ImageUrl };
            Response response = new Response(this, result);
            return await Task.FromResult(response);
        }
    }
}
