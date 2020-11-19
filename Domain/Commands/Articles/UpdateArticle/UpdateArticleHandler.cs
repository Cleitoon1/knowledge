using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Articles.UpdateArticle
{
    public class UpdateArticleHandler : Notifiable, IRequestHandler<UpdateArticleRequest, Response>
    {
        private readonly IArticleRep _articleRep;

        public UpdateArticleHandler(IArticleRep articleRep)
        {
            _articleRep = articleRep;
        }

        public async Task<Response> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }
            Article article = _articleRep.GetById(request.Id);
            if(article == null)
            {
                AddNotification("Article", "There is no article with this Id");
                return new Response(this);
            }

            article.UpdateArticle(request.Title, request.Description, request.ImageUrl, request.Content, request.CategoryId);            
            AddNotifications(article);

            if (Invalid)
                return new Response(this);

            article = _articleRep.Update(article);
            var result = new { article.Id, article.Title, article.Description, article.ImageUrl };
            Response response = new Response(this, result);
            return await Task.FromResult(response);
        }
    }
}
