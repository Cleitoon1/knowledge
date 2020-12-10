using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Categories.UpdateCategory
{
    public class UpdateCategoryHandler : Notifiable, IRequestHandler<UpdateCategoryRequest, Response>
    {
        private readonly ICategoryRep _categoryRep;

        public UpdateCategoryHandler(ICategoryRep categoryRep)
        {
            _categoryRep = categoryRep;
        }

        public async Task<Response> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }

            if (request.ParentId.HasValue && !_categoryRep.Exists(x => x.Id == request.ParentId.Value))
            {
                AddNotification("Category", "There is no category with this ParentCategoryId");
                return new Response(this);
            }

            Category category = _categoryRep.GetById(request.Id);
            if (category == null)
            {
                AddNotification("Category", "There is no Category with this Id");
                return new Response(this);
            }

            category.UpdateArticle(request.Name, request.ParentId);
            AddNotifications(category);

            if (Invalid)
                return new Response(this);

            category = _categoryRep.Update(category);
            var result = new { category.Id, category.Name, category.ParentId };
            Response response = new Response(this, result);
            return await Task.FromResult(response);
        }
    }
}
