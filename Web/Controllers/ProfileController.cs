using Data.Transactions;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/profiles/[action]")]
    public class ProfileController : BaseController
    {
        private IProfileRep _profileRep;

        public ProfileController(IUnitOfWork unitOfWork, IProfileRep profileRep) : base(unitOfWork)
        {
            _profileRep = profileRep;
        }

        public IActionResult All()
        {
            return Ok(_profileRep.GetAll());
        }
    }
}