
using Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ADO.NETCRUD.Jwt
{
    public class JwtSettings
    {
        public string CreateJwtToken(User users)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceretasd23123123wqaS21312QE2SQAE12E12");


            var identity = new ClaimsIdentity(new Claim[]
         {
                 new Claim(ClaimTypes.NameIdentifier,users.Id.ToString()!),
                new Claim (ClaimTypes.Role ,users.role),
                new Claim(ClaimTypes.Email,users.email)

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
