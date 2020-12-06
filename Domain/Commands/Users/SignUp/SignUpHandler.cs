using Domain.Interfaces.Repositories;
using Domain.Models;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Users.SignUp
{
    public class SignUpHandler : Notifiable, IRequestHandler<SignUpRequest, Response>
    {
        private readonly IUserRep _userRep;
        private readonly IProfileRep _profileRep;

        public SignUpHandler(IUserRep userRep, IProfileRep profileRep)
        {
            _userRep = userRep;
            _profileRep = profileRep;
        }

        public async Task<Response> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "The request data cannot be null");
                return new Response(this);
            }

            Profile profile = _profileRep.GetByName("User");
            User user = new User(request.Name, request.LastName, request.Mail, request.Password,
                request.MobilePhone, profile.Id, request.BirthDate);
            AddNotifications(user);
            if (Invalid)
                return new Response(this);
            User existing = _userRep.GetBy(x => x.Mail == request.Mail);
            if(existing != null)
            {
                AddNotification("Request", "There is user with this Mail");
                return new Response(this);
            }
            _userRep.Create(user);
            
            Response response = new Response(this, new { user.FullName, user.Mail });
            return await Task.FromResult(response);
        }
    }
}
