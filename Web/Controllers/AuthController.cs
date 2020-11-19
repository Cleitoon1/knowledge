using Data.Transactions;
using Domain.Commands.Users.SignUp;
using Domain.Interfaces.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Shared.Extensions;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserRep _userRep;

        public AuthController(IMediator mediator, IUserRep userRep, IUnitOfWork unitOfWork) : base(unitOfWork)

        {
            _mediator = mediator;
            _userRep = userRep;
        }
        [HttpPost]
        public IActionResult SignIn(string email, string password)
        {
            User user = _userRep.Login(email, password.ConvertToMD5());
            return Ok(user);
        }

        [HttpPost]
        public IActionResult SignUp(SignUpRequest data)
        {
            return Ok(_mediator.Send(data));
        }

        [HttpPost]
        public IActionResult SignOut()
        {

            return Ok();
        }

        [HttpGet]
        public IActionResult ValidateToken()
        {
            return Ok();
        }
    }
}
