using Data.Transactions;
using Domain.Commands.Categories.AddCategory;
using Domain.Commands.Categories.ListCategories;
using Domain.Commands.Categories.RemoveCategory;
using Domain.Commands.Categories.UpdateCategory;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{

    [Authorize("Administrador")]
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ICategoryRep _categoryRep;

        public CategoryController(IMediator mediator,ICategoryRep categoryRep, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mediator = mediator;
            _categoryRep = categoryRep;
        }

        [HttpGet]
        [Route("{parentCategoryId?}")]
        public IActionResult All(int? parentCategoryId = null)
        {
            return Ok(_mediator.Send(new ListCategoryRequest(parentCategoryId)));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_categoryRep.GetTree(id));
        }

        [HttpPost]
        [Authorize("Administrator")]
        public IActionResult Create(AddCategoryRequest data)
        {
            return Ok(_mediator.Send(data));
        }

        [HttpPost]
        [Authorize("Administrator")]
        public IActionResult Update(UpdateCategoryRequest data)
        {
            return Ok(_mediator.Send(data));
        }

        [HttpDelete]
        [Authorize("Administrator")]
        public IActionResult Delete(RemoveCategoryRequest data)
        {
            return Ok(_mediator.Send(data));
        }
    }
}
