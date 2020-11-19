using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web.Security
{
    public class SigningConfigurations
    {
        private const string SECRET_KEY = "31e2ec39-e533-4617-8fec-55494bad3214";
        public SigningCredentials SigningCredentials { get; }
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public SigningConfigurations()
        {
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256); ;
        }
    }
}
