using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Domain.Commands.Stats.GetStats
{
    public class GetStatsHandler : IRequestHandler<GetStatsRequest, GetStatsResponse>
    {
        private readonly IUserRep _userRep;
        private readonly ICategoryRep _categoryRep;
        private readonly IArticleRep _articleRep;

        public GetStatsHandler(IUserRep userRep, ICategoryRep categoryRep, IArticleRep articleRep)
        {
            _userRep = userRep;
            _categoryRep = categoryRep;
            _articleRep = articleRep;
        }

        public Task<GetStatsResponse> Handle(GetStatsRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetStatsResponse(
                _categoryRep.GetAll().Count(),
                _articleRep.GetAll().Count(),
                _userRep.GetAll().Count()
            ));
        }
    }
    
}