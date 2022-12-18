using lab7.Data;
using lab7.Models;
using lab7.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace lab7.Services
{
    public class UserService
    {
        private UsersContext _context;

        public UserService(UsersContext context)
        {
            _context = context;
        }

        public void Create(UserModel userModel)
        {
            var user = new User
            {
                Login = userModel.Login,
                PasswordHash = PasswordToHash(userModel.Password)
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public string? Token(string email, string password)
        {
            var identity = GetIdentity(email, password);
            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //var response = new
            //{
            //    access_token = encodedJwt
            //};

            return encodedJwt;//response.ToString();
        }

        public bool Contains(Func<User, bool> predicate)
        {
            var isExist = _context.Users.FirstOrDefault(predicate);
            if (isExist == null)
                return false;
            else
                return true;
        }
        private ClaimsIdentity GetIdentity(string login, string password)
        {
            User? person = FindUser(login, password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private User? FindUser(string login, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login && 
                                                      u.PasswordHash == PasswordToHash(password));
        }

        private string PasswordToHash(string password)
        {
            using (var hashAlg = MD5.Create())
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

    }
}
