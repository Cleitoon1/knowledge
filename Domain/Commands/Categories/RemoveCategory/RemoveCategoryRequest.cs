using MediatR;

namespace Domain.Commands.Categories.RemoveCategory
{
    public class RemoveCategoryRequest : IRequest<Response>
    {
        public RemoveCategoryRequest(long id)
        {
            Id = id;
        }
        public long Id { get; set; }        
    }
}
