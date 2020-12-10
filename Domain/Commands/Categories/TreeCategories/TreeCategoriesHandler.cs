using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using MediatR;
using Flunt.Notifications;

namespace Domain.Commands.Categories.TreeCategories
{
    public class TreeCategoriesHandler
        : Notifiable, IRequestHandler<TreeCategoriesRequest, Response>
    {
        private readonly ICategoryRep _categoryRep;

        public TreeCategoriesHandler(ICategoryRep categoryRep)
        {
            _categoryRep = categoryRep;
        }

        public Task<Response> Handle(TreeCategoriesRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<TreeCategoriesResponse> data = _categoryRep.GetTree(request.parentCategoryId)
                .Select(x => new TreeCategoriesResponse(x)).ToList();
            Response response = new Response(this, data);
            return Task.FromResult(response);
        
        }
    }
}