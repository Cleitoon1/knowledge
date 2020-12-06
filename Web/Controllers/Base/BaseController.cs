using Data.Transactions;
using Domain.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Base
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        internal IActionResult HandleResponse(Response response)
        {
            if (!response.Notifications.Any())
            {
                try
                {
                    _unitOfWork.SaveChanges();

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista. Erro interno: {ex.Message}");
                }
            }
            else
            {
                if(response.Success)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
        }

        internal IActionResult HandleException(Exception ex)
        {
            return BadRequest(new { errors = ex.Message, exception = ex.ToString() });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
