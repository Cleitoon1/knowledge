using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands.Articles.GetArticleById
{
    public class GetArticleByIdRequest : IRequest<Response>
    {
        public int Id { get; set; }

        public GetArticleByIdRequest(int id)
        {
            Id = id;
        }
    }
}
