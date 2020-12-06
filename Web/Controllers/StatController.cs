using System.Threading.Tasks;
using Data.Transactions;
using Domain.Commands.Stats.GetStats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/stats/[action]")]
    public class StatController : BaseController
    {
        private readonly IMediator _mediator;

        public StatController(IUnitOfWork unitOfWork, IMediator mediator) : base(unitOfWork)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            return Ok(await _mediator.Send(new GetStatsRequest()));
        }
    }
}