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

namespace Web.Controllers
{
    [Authorize("Administrador")]
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

        [HttpGet("Get")]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_userRep.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(User data)
        {
            data.Validate();
            if (data.Valid)
            {
                _userRep.Create(data);
                data.CleanPassword();
                return Ok(data);
            }
            else
                return BadRequest(new Response(data));
        }

        [HttpPost]
        public IActionResult Update(User data)
        {
            data.Validate();
            if (data.Valid)
            {
                _userRep.Create(data);
                data.CleanPassword();
                return Ok();
            }
            else
                return BadRequest(new Response(data));
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            _userRep.Delete(id);
            return Ok();
        }
    }
}
