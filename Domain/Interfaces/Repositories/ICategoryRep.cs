using Domain.Interfaces.Repositories.Base;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ICategoryRep : IBaseRep<Category, long>
    {
        Category GetTree(long id);
    }
}
