using System;
using MediatR;

namespace Domain.Commands.Users.SignUp
{
    public class SignUpRequest : IRequest<Response>
    {
        public SignUpRequest()
        {
            
        }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? BirthDate { get; set; }
        public long ProfileId { get;  set; }
    }
}
