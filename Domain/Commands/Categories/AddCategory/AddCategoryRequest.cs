using MediatR;

namespace Domain.Commands.Categories.AddCategory
{
    public class AddCategoryRequest : IRequest<Response>
    {
        public AddCategoryRequest(string name, long? parentId)
        {
            Name = name;
            ParentId = parentId;
        }

        public string Name { get; set; }
        public long? ParentId { get; set; }
    }
}
