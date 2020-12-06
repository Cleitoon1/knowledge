using System.Threading.Tasks;
using Data.Transactions;
using Domain.Commands.Articles.AddArticle;
using Domain.Commands.Articles.GetArticleById;
using Domain.Commands.Articles.ListArticles;
using Domain.Commands.Articles.RemoveArticle;
using Domain.Commands.Articles.UpdateArticle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{

    [Authorize]
    [Route("api/articles/[action]")]
    public class ArticleController : BaseController
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator,IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All(ListArticleRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(GetArticleByIdRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddArticleRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateArticleRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveArticleRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }
    }
}
