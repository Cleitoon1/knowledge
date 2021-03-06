﻿using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Categories.ListCategories
{
    public class ListCategoryHandler : Notifiable, IRequestHandler<ListCategoryRequest, Response>
    {
        private readonly ICategoryRep _categoryRep;

        public ListCategoryHandler(ICategoryRep categoryRep)
        {
            _categoryRep = categoryRep;
        }

        public async Task<Response> Handle(ListCategoryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }

            IEnumerable<long> lstIds = (request.ParentId.HasValue ?
                _categoryRep.GetAll(x => x.ParentId == request.ParentId.Value, tracking: false) 
                : _categoryRep.GetAll(false)).Select(x => x.Id).ToList();
            Response response = new Response(this, lstIds.Select(x => new ListArticleResponse(_categoryRep.GetPath(x))));
            return await Task.FromResult(response);
        }
    }
}
