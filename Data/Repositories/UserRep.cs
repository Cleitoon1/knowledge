using Data.Repositories.Base;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Base;
using Domain.Models;
using System.Linq;

namespace Data.Repositories
{
    public class UserRep : BaseRep<User, long>, IBaseRep<User, long>, IUserRep
    {
        public UserRep(KnowledgeContext context) : base(context)
        {
        }

        public User Login(string mail, string password)
        {
            return _context.Set<User>().Where(x => x.Mail == mail && x.Password == password).FirstOrDefault();
        }
    }
}
