using System.Linq;
using Data.Repositories.Base;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Data.Repositories
{
    public class ProfileRep : BaseRep<Profile, long>, IProfileRep
    {
        public ProfileRep(KnowledgeContext context) : base(context)
        {
        }

        public Profile GetByName(string name)
        {
            return GetBy(x => x.Name == name);
        }
    }
}
