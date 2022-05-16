using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User;

namespace ResturantAPI.Repository
{
    public class JWTManagerRepo : IJWTManagerRepo
    {
        private IConfiguration configuration;
        private UserLogic userLogic;

        public List<UserInfo> users = new List<UserInfo>();
        public JWTManagerRepo (IConfiguration configuration, UserLogic userLogic)
        {
            this.configuration = configuration;
            this.userLogic = userLogic;
        }
        public Tokens Authenticate(UserInfo user)
        {
            users = userLogic.GetAllUsers();
            if (!users.Any(a => 
            a.UserName == user.UserName &&
            a.Password == user.Password &&
            a.IsAdmin == user.IsAdmin))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            Claim temp = new Claim(ClaimTypes.Role, "normal");
            if (user.UserName == "Admin")
                temp = new Claim(ClaimTypes.Role, Convert.ToString(user.UserName));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        temp
                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };

            throw new NotImplementedException();
        }
    }
}
