using MediatR;

namespace Domain.Commands.Categories.ListCategories
{
    public class ListCategoryRequest : IRequest<Response>
    {
        public int? ParentId { get; set; }

        public ListCategoryRequest(int? parentId)
        {
            ParentId = parentId;
        }

        public ListCategoryRequest()
        {

        }
    }
}
