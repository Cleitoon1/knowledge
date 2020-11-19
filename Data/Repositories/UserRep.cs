using Data.Repositories.Base;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Base;
using Domain.Models;

namespace Data.Repositories
{
    public class UserRep : BaseRep<User, long>, IBaseRep<User, long>, IUserRep
    {
        public UserRep(KnowledgeContext context) : base(context)
        {
        }

        public User Login(string email, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
