using MediatR;

namespace Domain.Commands.Categories.ListCategories
{
    public class ListCategoryRequest : IRequest<Response>
    {
        public int? ParentCategoryId { get; set; }

        public ListCategoryRequest(int? parentCategoryId)
        {
            ParentCategoryId = parentCategoryId;
        }

        public ListCategoryRequest()
        {

        }
    }
}
