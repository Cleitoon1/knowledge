using MediatR;

namespace Domain.Commands.Categories.UpdateCategory
{
    public class UpdateCategoryRequest : IRequest<Response>
    {
        public UpdateCategoryRequest(long id, string name, long? parentId)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
    }
}
