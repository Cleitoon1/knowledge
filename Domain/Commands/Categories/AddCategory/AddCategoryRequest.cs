using MediatR;

namespace Domain.Commands.Categories.AddCategory
{
    public class AddCategoryRequest : IRequest<Response>
    {
        public AddCategoryRequest(string name, long? parentCategoryId)
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
        }

        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
