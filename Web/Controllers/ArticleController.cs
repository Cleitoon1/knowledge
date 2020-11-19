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
    public class ArticleController : BaseController
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator,IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult All(ListArticleRequest data)
        {
            return Ok(_mediator.Send(data));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(GetArticleByIdRequest data)
        {
            return Ok(_mediator.Send(data));
        }

        [HttpPost]
        [Authorize("Administrator")]
        public IActionResult Create(AddArticleRequest data)
        {
            return Ok(_mediator.Send(data));
        }

        [HttpPost]
        [Authorize("Administrator")]
        public IActionResult Update(UpdateArticleRequest data)
        {
            return Ok(_mediator.Send(data));
        }

        [HttpDelete]
        [Authorize("Administrator")]
        public IActionResult Delete(RemoveArticleRequest data)
        {
            return Ok(_mediator.Send(data));
        }
    }
}
