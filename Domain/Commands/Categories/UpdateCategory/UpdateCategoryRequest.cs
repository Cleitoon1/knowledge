using MediatR;

namespace Domain.Commands.Categories.UpdateCategory
{
    public class UpdateCategoryRequest : IRequest<Response>
    {
        public UpdateCategoryRequest(long id, string name, long? parentCategoryId)
        {
            Id = id;
            Name = name;
            ParentCategoryId = parentCategoryId;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
