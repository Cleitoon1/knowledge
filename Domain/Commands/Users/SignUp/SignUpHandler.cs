using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Users.SignUp
{
    public class SignUpHandler : IRequestHandler<SignUpRequest, Response>
    {
        public Task<Response> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
