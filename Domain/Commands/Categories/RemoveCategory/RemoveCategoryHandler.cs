using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Categories.RemoveCategory
{
    class RemoveCategoryHandler : Notifiable, IRequestHandler<RemoveCategoryRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly ICategoryRep _categoryRep;
        private readonly IArticleRep _articleRep;

        public RemoveCategoryHandler(IMediator mediator, ICategoryRep categoryRep, IArticleRep articleRep)
        {
            _mediator = mediator;
            _categoryRep = categoryRep;
            _articleRep = articleRep;
        }

        public async Task<Response> Handle(RemoveCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }

            if (!_categoryRep.Exists(x => x.Id == request.Id))
            {
                AddNotification("Category", "There is no category with this Id");
                return new Response(this);
            }

            if(_articleRep.Exists(x => x.CategoryId == request.Id))
            {
                AddNotification("Category", "There is no articles with this Category");
                return new Response(this);
            }

            _categoryRep.Delete(request.Id);
            return await Task.FromResult(new Response(this, new { request.Id }));
        }
    }
}
