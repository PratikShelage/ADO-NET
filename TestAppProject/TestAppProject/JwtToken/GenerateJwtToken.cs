using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestAppProject.Model;

namespace TestAppProject.JwtToken
{
    public class GenerateJwtToken
    {

        public string createJWtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceretasd23123123wqaS21312QE2SQAE12E12");


            var identity = new ClaimsIdentity(new Claim[]
         { 
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()!),
                new Claim (ClaimTypes.Role ,user.role!),
                new Claim(ClaimTypes.Email,user.email)
         });
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var Token = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = creds

            };

            var token = jwtTokenHandler.CreateToken(Token);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
