using System.Threading.Tasks;
using Data.Transactions;
using Domain.Commands.Categories.AddCategory;
using Domain.Commands.Categories.ListCategories;
using Domain.Commands.Categories.RemoveCategory;
using Domain.Commands.Categories.TreeCategories;
using Domain.Commands.Categories.UpdateCategory;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{

    [Authorize]
    [Route("api/categories/[action]")]
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
        [Route("{parentId?}")]
        public async Task<IActionResult> All(int? parentId = null)
        {
            return HandleResponse(await _mediator.Send(new ListCategoryRequest(parentId)));
        }

        [HttpGet]
        public async Task<IActionResult> GetTree()
        {
            return Ok(await _mediator.Send(new TreeCategoriesRequest()));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_categoryRep.GetPath(id));
        }

        [Authorize("Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(AddCategoryRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }

        [Authorize("Administrator")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }

        [Authorize("Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return HandleResponse(await _mediator.Send(new RemoveCategoryRequest(id)));
        }
    }
}
