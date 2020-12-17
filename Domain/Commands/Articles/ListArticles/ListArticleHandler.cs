using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Articles.ListArticles
{
    public class ListArticleHandler : Notifiable, IRequestHandler<ListArticleRequest, Response>
    {
        private readonly IArticleRep _articleRep;

        public ListArticleHandler(IArticleRep articleRep)
        {
            _articleRep = articleRep;
        }

        public async Task<Response> Handle(ListArticleRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }

            IEnumerable<Article> data = (request.CategoryId.HasValue ?
                _articleRep.GetAllBy(x => x.CategoryId == request.CategoryId.Value, tracking: false, 
                  x => x.User) :_articleRep.GetAll()).ToList();
            if(request.Page.HasValue && request.Quantity.HasValue)
                data = data.Skip(request.Page.Value * request.Quantity.Value - request.Quantity.Value)
                    .Take(request.Quantity.Value).ToList();
            Response response = new Response(this, data);
            return await Task.FromResult(response);
        }
    }
}
