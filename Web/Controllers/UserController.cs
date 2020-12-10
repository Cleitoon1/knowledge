using Data.Transactions;
using Domain.Commands;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize("Administrator")]
    [Route("api/users/[action]")]
    public class UserController : BaseController
    {
        private readonly IUserRep _userRep;

        public UserController(IUserRep userRep, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _userRep = userRep;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(_userRep.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            User user = _userRep.GetById(id);
            user.CleanPassword();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(UserVM model)
        {
            User data = new User(model.Name, model.LastName, model.Mail, model.Password, model.MobilePhone,
             model.ProfileId, model.BirthDate);
            if (data.Valid)
            {
                _userRep.Create(data);
                data.CleanPassword();
                return HandleResponse(new Response(data));
            }
            else
                return BadRequest(new Response(data));
        }

        [HttpPost]
        public IActionResult Update(UserVM model)
        {
            User data = new User(model.Id, model.Name, model.LastName, model.Mail, model.Password,
             model.MobilePhone, model.ProfileId, model.BirthDate);
            if (data.Valid)
            {
                _userRep.Update(data);
                return HandleResponse(new Response(data));
            }
            else
                return BadRequest(new Response(data));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _userRep.Delete(id);
            return HandleResponse(new Response(true, null));
        }
    }
}
