using Domain.Interfaces.Repositories.Base;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IProfileRep : IBaseRep<Profile, long>
    {
        Profile GetByName(string name);
    }
}
