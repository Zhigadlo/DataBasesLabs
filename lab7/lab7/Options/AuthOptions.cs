using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace lab7.Options
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthIssuer";
        public const string AUDIENCE = "AuthClient";
        const string KEY = "secretkey1234_security_505";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
