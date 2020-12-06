using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Categories.AddCategory
{
    public class AddCategoryHandler : Notifiable, IRequestHandler<AddCategoryRequest, Response>
    {
        private readonly ICategoryRep _categoryRep;

        public AddCategoryHandler(ICategoryRep categoryRep)
        {
            _categoryRep = categoryRep;
        }

        public async Task<Response> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }
            
            if(request.ParentCategoryId.HasValue && !_categoryRep.Exists(x => x.Id == request.ParentCategoryId.Value))
            {
                AddNotification("Request", "There is no category with this ParentCategoryId");
                return new Response(this);
            }

            Category category = new Category(request.Name, request.ParentCategoryId);
            category.Validate();
            AddNotifications(category);

            if (Invalid)
                return new Response(this);

            category = _categoryRep.Create(category);
            var result = new { category.Id, category.Name };
            Response response = new Response(this, result);
            return await Task.FromResult(response);
        }
    }
}
