using Data.Transactions;
using Domain.Commands.Users.SignUp;
using Domain.Interfaces.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Domain.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Web.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Domain.Commands;
using Microsoft.Extensions.Options;
using Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserRep _userRep;
        private readonly IProfileRep _profileRep;
        
        public AuthController(IMediator mediator, IProfileRep profileRep,
            IUserRep userRep, IUnitOfWork unitOfWork) : base(unitOfWork)

        {
            _mediator = mediator;
            _userRep = userRep;
            _profileRep = profileRep;
        }

        [HttpPost]
        public IActionResult SignIn(LoginVM data,
        [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                User user = _userRep.Login(data.Mail, data.Password.ConvertToMD5());
                if(user == null)
                    return NotFound("User not found");

                Profile profile = _profileRep.GetById(user.ProfileId);
                DateTime creationDate = DateTime.UtcNow;
                DateTime expirationDate = creationDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);
                SymmetricSecurityKey signingKey = 
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfigurations.PrivateKey));
                var tokenDescriptor = new SecurityTokenDescriptor {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(ClaimTypes.Email, user.Mail),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Role, profile.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }),
                    NotBefore = creationDate,
                    Expires = expirationDate,
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                };

                SecurityToken token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new UserDataVM(token.ValidFrom, token.ValidTo, tokenstring, "OK",
                    user.Name, user.LastName, profile.Name));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpRequest data)
        {
            return HandleResponse(await _mediator.Send(data));
        }
    }
}
