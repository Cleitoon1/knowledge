using Domain.Interfaces.Repositories.Base;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRep : IBaseRep<User, long>
    {
        User Login(string mail, string password);
    }
}
